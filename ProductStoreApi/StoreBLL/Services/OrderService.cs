
using AutoMapper;
using StoreBLL.Interfaces.Services;
using StoreBLL.Models;
using StoreBLL.Services.Abstractions;
using StoreDAL.Entities;
using StoreDAL.Interfaces;

namespace StoreBLL.Services
{
	public class OrderService : AbstractAdminPanelItem<Order, OrderModel>, IOrderService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		static readonly long CancelStatusId = 4;

		public OrderService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork.OrderRepository)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public Task CancelOrder(long id)
		{
			return ChangeStatus(id, CancelStatusId);
		}

		public async Task ChangeStatus(long orderId, long statusId)
		{
			var entity = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);

			entity.StatusId = statusId;

			await _unitOfWork.OrderRepository.Update(entity);
			await _unitOfWork.SaveAsync();
		}

		public async Task<IEnumerable<OrderDetailModel>> GetDetails(long orderId)
		{
			var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
			return _mapper.Map<IList<OrderDetailModel>>(order.Details);
		}

		public async Task<IEnumerable<OrderModel>> GetUserOrders(long userId)
		{
			var entities = await _unitOfWork.OrderRepository.GetByUser(userId);
			return _mapper.Map<IList<OrderModel>>(entities);
		}
	}
}
