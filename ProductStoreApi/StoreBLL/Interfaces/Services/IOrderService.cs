using StoreBLL.Models;

namespace StoreBLL.Interfaces.Services
{
    public interface IOrderService : IAdminPanelItem<OrderModel>
    {
		Task<IEnumerable<OrderDetailModel>> GetDetails(long orderId);
		Task<IEnumerable<OrderModel>> GetUserOrders(long userId);

		Task ChangeStatus(long orderId, long statusId);
		Task CancelOrder(long id);
	}
}
