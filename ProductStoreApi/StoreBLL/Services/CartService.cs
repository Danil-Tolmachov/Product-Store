
using AutoMapper;
using StoreBLL.Interfaces.Services;
using StoreBLL.Models;
using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Interfaces;

namespace StoreBLL.Services
{
	public class CartService : ICartService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CartService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task ClearUserCart(long userId)
		{
			await _unitOfWork.CartRepository.ClearCartByUserId(userId);
			await _unitOfWork.SaveAsync();
		}

		public async Task<IEnumerable<CartItemModel>> GetUserProducts(long userId)
		{
			var entities = await _unitOfWork.CartRepository.GetUserProducts(userId);
			return _mapper.Map<IList<CartItemModel>>(entities);
		}

		public async Task AddProduct(CartItemModel item, long userId)
		{
			try
			{
				// Throw if item not found
				CartItem entity = await _unitOfWork.CartItemRepository.GetByIdAsync(item.CartId, item.ProductId);

				entity.Quantity = item.Quantity;
				await _unitOfWork.CartItemRepository.Update(entity);
			}
			catch
			{
				// If item does not exists
				var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
				CartItem entity = _mapper.Map<CartItem>(item);

				entity.Cart = user.Cart;

				await _unitOfWork.CartItemRepository.AddAsync(entity);
			}

			await _unitOfWork.SaveAsync();
		}

		public async Task RemoveProduct(ProductModel product, long userId)
		{
			var cart = await _unitOfWork.CartRepository.GetCartByUserId(userId);

			await _unitOfWork.CartItemRepository.DeleteByIdAsync(cart.Id, product.Id);
			await _unitOfWork.SaveAsync();
		}

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
