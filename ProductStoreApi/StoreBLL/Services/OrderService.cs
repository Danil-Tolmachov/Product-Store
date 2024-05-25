
using AutoMapper;
using StoreBLL.Interfaces.Services;
using StoreBLL.Models;
using StoreBLL.Models.Extra;
using StoreBLL.Services.Abstractions;
using StoreDAL.Entities;
using StoreDAL.Interfaces;

namespace StoreBLL.Services
{
	/// <summary>
	/// Provides services for managing orders.
	/// </summary>
	public class OrderService : AbstractAdminPanelItem<Order, OrderModel>, IOrderService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		static readonly long submitedStatusId = StatusConfiguration.SubmitedStatusId;
		static readonly long completedStatusId = StatusConfiguration.CompletedStatusId;
		static readonly long canceledStatusId = StatusConfiguration.CanceledStatusId;

		public OrderService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork.OrderRepository)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		/// <summary>
		/// Cancels the order with the specified identifier.
		/// </summary>
		/// <param name="id">The order identifier.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		public Task CancelOrder(long id)
		{
			return ChangeStatus(id, canceledStatusId);
		}

		/// <summary>
		/// Completes the order with the specified identifier.
		/// </summary>
		/// <param name="id">The order identifier.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		public Task CompleteOrder(long id)
		{
			return ChangeStatus(id, completedStatusId);
		}

		/// <summary>
		/// Changes the status of the order with the specified identifier.
		/// </summary>
		/// <param name="orderId">The order identifier.</param>
		/// <param name="statusId">The new status identifier.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		public async Task ChangeStatus(long orderId, long statusId)
		{
			var entity = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);

			entity.StatusId = statusId;

			await _unitOfWork.OrderRepository.Update(entity);
			await _unitOfWork.SaveAsync();
		}

		/// <summary>
		/// Gets the details of the order with the specified identifier.
		/// </summary>
		/// <param name="orderId">The order identifier.</param>
		/// <returns>An enumerable collection of order detail models.</returns>
		public async Task<IEnumerable<OrderDetailModel>> GetDetails(long orderId)
		{
			var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
			return _mapper.Map<IList<OrderDetailModel>>(order.Details);
		}

		/// <summary>
		/// Gets the orders of the user with the specified identifier.
		/// </summary>
		/// <param name="userId">The user identifier.</param>
		/// <returns>An enumerable collection of order models.</returns>
		public async Task<IEnumerable<OrderModel>> GetUserOrders(long userId)
		{
			var entities = await _unitOfWork.OrderRepository.GetByUser(userId);
			return _mapper.Map<IList<OrderModel>>(entities);
		}

		/// <summary>
		/// Submits a new order based on the given model.
		/// </summary>
		/// <param name="model">The model containing the order details.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		public async Task SubmitCart(SubmitOrderModel model)
		{
			Employee employee = await _unitOfWork.EmployeeRepository.GetWithLeastOrders();

			var orderDetailModels = _mapper.Map<IList<OrderDetailModel>>(model.CartItems);
			var orderDetailEntities = _mapper.Map<IList<OrderDetail>>(orderDetailModels);

			Order entity = new(0)
			{
				UserId = model.UserId,
				EmployeeId = employee.Id,
				StatusId = submitedStatusId,
				UserComment = model.UserComment,
				Details = orderDetailEntities,
			};

			await _unitOfWork.OrderRepository.AddAsync(entity);
			await _unitOfWork.SaveAsync();
		}

		/// <summary>
		/// Clears the cart of the user with the specified identifier.
		/// </summary>
		/// <param name="userId">The user identifier.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		public async Task ClearCart(long userId)
		{
			await _unitOfWork.CartRepository.ClearCartByUserId(userId);
			await _unitOfWork.SaveAsync();
		}
	}
}
