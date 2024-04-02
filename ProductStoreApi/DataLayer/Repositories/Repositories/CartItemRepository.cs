using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Interfaces.Repositories;

namespace StoreDAL.Repositories.Repositories
{
	public class CartItemRepository : AbstractRepository<CartItem>, ICartItemRepository
	{
		public CartItemRepository(StoreDbContext context) : base(context) { }
	}
}
