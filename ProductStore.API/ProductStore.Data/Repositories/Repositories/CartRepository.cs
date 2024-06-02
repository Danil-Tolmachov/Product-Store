using Microsoft.EntityFrameworkCore;
using ProductStore.Data.Entities;
using ProductStore.Data.Infrastructure;
using ProductStore.Data.Interfaces.Repositories;

namespace ProductStore.Data.Repositories.Repositories
{
	public class CartRepository : AbstractSingleKeyRepository<Cart>, ICartRepository
	{
		public CartRepository(StoreDbContext context) : base(context) { }

		public async Task ClearCartByUserId(long userId)
		{
			var cart = await dbSet.SingleAsync(c => c.UserId == userId);

			foreach (var item in cart.CartItems.ToList())
			{
				context.CartItems.Remove(item);
			}
		}

		public async Task<Cart> GetCartByUserId(long userId)
		{
			return await dbSet.Include(c => c.CartItems)
							  .ThenInclude(ci => ci.Product)
							  .ThenInclude(p => p.Category)
							  .Include(c => c.CartItems)
							  .ThenInclude(ci => ci.Product)
							  .ThenInclude(p => p.Images)
							  .AsSplitQuery()
							  .SingleAsync(c => c.UserId == userId);
		}

		public async Task<IEnumerable<CartItem>> GetUserProducts(long userId)
		{
			var cart = await dbSet.Include(c => c.CartItems)
								  .ThenInclude(ci => ci.Product)
								  .ThenInclude(p => p.Category)
								  .Include(c => c.CartItems)
								  .ThenInclude(ci => ci.Product)
								  .ThenInclude(p => p.Images)
								  .AsSplitQuery()
								  .SingleAsync(c => c.UserId == userId);

			return cart.CartItems.ToList();
		}
	}
}
