using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Interfaces.Repositories;

namespace StoreDAL.Repositories.Repositories
{
	public class OrderRepository : AbstractSingleKeyRepository<Order>, IOrderRepository
	{
		public OrderRepository(StoreDbContext context) : base(context) { }
	}
}
