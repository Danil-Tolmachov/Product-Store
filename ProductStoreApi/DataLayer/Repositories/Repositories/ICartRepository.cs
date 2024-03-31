using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Repositories;

namespace StoreDAL.Interfaces.Repositories
{
	public class CartRepository : AbstractRepository<Cart>, ICartRepository
	{
		public CartRepository(StoreDbContext context) : base(context) { }
	}
}
