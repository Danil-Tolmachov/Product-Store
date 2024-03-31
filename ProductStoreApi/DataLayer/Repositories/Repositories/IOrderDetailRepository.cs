using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Repositories;

namespace StoreDAL.Interfaces.Repositories
{
	public class OrderDetailRepository : AbstractRepository<OrderDetail>, IOrderDetailRepository
	{
		public OrderDetailRepository(StoreDbContext context) : base(context) { }
	}
}
