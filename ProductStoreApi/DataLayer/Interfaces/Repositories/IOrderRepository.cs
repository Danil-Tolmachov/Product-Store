using StoreDAL.Entities;

namespace StoreDAL.Interfaces.Repositories
{
	public interface IOrderRepository : ISingleKeyRepository<Order>
	{
		Task<IEnumerable<Order>> GetByUser(long userId);
	}
}
