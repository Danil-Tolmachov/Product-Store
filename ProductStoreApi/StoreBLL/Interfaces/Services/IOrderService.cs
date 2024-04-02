﻿using StoreBLL.Models;

namespace StoreBLL.Interfaces.Services
{
    public interface IOrderService : IAdminPanelItem<OrderModel>
    {
		Task<IEnumerable<ProductModel>> GetProducts(long id);
		Task<IEnumerable<OrderModel>> GetUserOrders(long userId);

		Task CancelOrder(long id);
	}
}