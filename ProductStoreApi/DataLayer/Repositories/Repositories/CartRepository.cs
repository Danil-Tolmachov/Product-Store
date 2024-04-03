using Microsoft.EntityFrameworkCore;
using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Interfaces.Repositories;

namespace StoreDAL.Repositories.Repositories
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
			return await dbSet.SingleAsync(c => c.UserId == userId);
		}

		public async Task<IEnumerable<CartItem>> GetUserProducts(long userId)
		{
			var cart = await dbSet.SingleAsync(c => c.UserId == userId);

			return cart.CartItems.ToList();
		}
	}
}
