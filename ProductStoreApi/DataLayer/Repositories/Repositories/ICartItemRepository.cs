using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Repositories;

namespace StoreDAL.Interfaces.Repositories
{
	public class CartItemRepository : AbstractRepository<CartItem>, ICartItemRepository
	{
		public CartItemRepository(StoreDbContext context) : base(context) { }
	}
}
