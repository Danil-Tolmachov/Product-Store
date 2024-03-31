using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Repositories;

namespace StoreDAL.Interfaces.Repositories
{
	public class OrderRepository : AbstractRepository<Order>, IOrderRepository
	{
		public OrderRepository(StoreDbContext context) : base(context) { }
	}
}
