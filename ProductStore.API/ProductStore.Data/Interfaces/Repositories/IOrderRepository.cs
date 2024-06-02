using ProductStore.Data.Entities;

namespace ProductStore.Data.Interfaces.Repositories
{
	public interface IOrderRepository : ISingleKeyRepository<Order>
	{
		Task<IEnumerable<Order>> GetByUser(long userId);
	}
}
