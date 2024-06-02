using ProductStore.Business.Models;
using ProductStore.Business.Models.Extra;

namespace ProductStore.Business.Interfaces.Services
{
    public interface IOrderService : IAdminPanelItem<OrderModel>
    {
		Task<IEnumerable<OrderDetailModel>> GetDetails(long orderId);
		Task<IEnumerable<OrderModel>> GetUserOrders(long userId);

		Task SubmitCart(SubmitOrderModel model);
		Task ClearCart(long userId);

		Task ChangeStatus(long orderId, long statusId);
		Task CancelOrder(long id);
	}
}
