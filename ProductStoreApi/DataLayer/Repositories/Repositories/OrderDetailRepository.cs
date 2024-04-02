using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Interfaces.Repositories;

namespace StoreDAL.Repositories.Repositories
{
	public class OrderDetailRepository : AbstractRepository<OrderDetail>, IOrderDetailRepository
	{
		public OrderDetailRepository(StoreDbContext context) : base(context) { }
	}
}
