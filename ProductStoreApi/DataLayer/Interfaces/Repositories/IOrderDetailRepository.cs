using StoreDAL.Entities;

namespace StoreDAL.Interfaces.Repositories
{
	public interface IOrderDetailRepository : IRepository<OrderDetail>
	{
		Task<OrderDetail> GetByIdAsync(long orderId, long productId);
		Task DeleteByIdAsync(long orderId, long productId);
	}
}
