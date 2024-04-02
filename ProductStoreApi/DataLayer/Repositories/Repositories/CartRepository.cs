using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Interfaces.Repositories;

namespace StoreDAL.Repositories.Repositories
{
	public class CartRepository : AbstractRepository<Cart>, ICartRepository
	{
		public CartRepository(StoreDbContext context) : base(context) { }
	}
}
