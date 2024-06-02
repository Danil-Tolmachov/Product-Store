
using AutoMapper;
using ProductStore.Business.Interfaces.Services;
using ProductStore.Business.Models;
using ProductStore.Data.Entities;
using ProductStore.Data.Interfaces;

namespace ProductStore.Business.Services
{
	/// <summary>
	/// Provides services for managing the shopping cart.
	/// </summary>
	public class CartService : ICartService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CartService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		/// <summary>
		/// Clears the user's shopping cart.
		/// </summary>
		/// <param name="userId">The user identifier.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		public async Task ClearUserCart(long userId)
		{
			await _unitOfWork.CartRepository.ClearCartByUserId(userId);
			await _unitOfWork.SaveAsync();
		}

		/// <summary>
		/// Gets the products in the user's shopping cart.
		/// </summary>
		/// <param name="userId">The user identifier.</param>
		/// <returns>An enumerable collection of cart item models.</returns>
		public async Task<IEnumerable<CartItemModel>> GetUserProducts(long userId)
		{
			var entities = await _unitOfWork.CartRepository.GetUserProducts(userId);
			return _mapper.Map<IList<CartItemModel>>(entities);
		}

		/// <summary>
		/// Gets the user's shopping cart.
		/// </summary>
		/// <param name="userId">The user identifier.</param>
		/// <returns>The cart model.</returns>
		public async Task<CartModel> GetUserCart(long userId)
		{
			var entity = await _unitOfWork.CartRepository.GetCartByUserId(userId);
			return _mapper.Map<CartModel>(entity);
		}

		/// <summary>
		/// Adds a product to the user's shopping cart.
		/// </summary>
		/// <param name="model">The cart item model.</param>
		/// <param name="userId">The user identifier.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		public async Task AddProduct(CartItemModel model, long userId)
		{
			try
			{
				// Throw if item not found
				CartItem entity = await _unitOfWork.CartItemRepository.GetByIdAsync(model.CartId, model.ProductId);

				entity.Quantity = model.Quantity;
				await _unitOfWork.CartItemRepository.Update(entity);
			}
			catch
			{
				// If item does not exists
				var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);

				CartItem entity = new (0)
				{
					CartId = user.Cart.Id,
					ProductId = model.ProductId,
					Quantity = model.Quantity,
				};

				await _unitOfWork.CartItemRepository.AddAsync(entity);
			}

			await _unitOfWork.SaveAsync();
		}

		/// <summary>
		/// Removes a product from the user's shopping cart.
		/// </summary>
		/// <param name="product">The product model.</param>
		/// <param name="userId">The user identifier.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		public async Task RemoveProduct(ProductModel product, long userId)
		{
			var cart = await _unitOfWork.CartRepository.GetCartByUserId(userId);

			await _unitOfWork.CartItemRepository.DeleteByIdAsync(cart.Id, product.Id);
			await _unitOfWork.SaveAsync();
		}

		/// <summary>
		/// Submits the order for the user.
		/// </summary>
		/// <param name="userId">The user identifier.</param>
		/// <param name="userComment">The user's comment for the order.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		public async Task SubmitOrder(long userId, string? userComment)
		{
			var cart = await _unitOfWork.CartRepository.GetCartByUserId(userId);
			var employee = await _unitOfWork.EmployeeRepository.GetWithLeastOrders();
			long id = await _unitOfWork.OrderRepository.Count();

			// Add Order
			var order = new Order(id)
			{
				UserId = userId,
				EmployeeId = employee.Id,
				StatusId = 1
			};

			if (userComment is not null)
			{
				order.UserComment = userComment;
			}

			await _unitOfWork.OrderRepository.AddAsync(order);

			// Add Details
			foreach (var item in cart.CartItems)
			{
				var entity = _mapper.Map<OrderDetail>(item);
				await _unitOfWork.OrderDetailRepository.AddAsync(entity);
			}

			await _unitOfWork.SaveAsync();
		}
	}
}
