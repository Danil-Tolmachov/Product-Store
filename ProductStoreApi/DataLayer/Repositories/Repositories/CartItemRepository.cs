using Microsoft.EntityFrameworkCore;
using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Interfaces.Repositories;

namespace StoreDAL.Repositories.Repositories
{
	public class CartItemRepository : AbstractRepository<CartItem>, ICartItemRepository
	{
		public CartItemRepository(StoreDbContext context) : base(context)
		{
		}

		public async Task<CartItem> GetByIdAsync(long cartId, long productId)
		{
			try
			{
				return await dbSet
					.SingleAsync(e => e.ProductId == productId && e.CartId == cartId);
			}
			catch (InvalidOperationException ex)
			{
				throw new ArgumentException("Item with provided id's does not exist", ex);
			}
		}

		public override async Task Update(CartItem entity)
		{
			var existingEntity = await dbSet.SingleAsync(x => x.CartId == entity.CartId && x.ProductId == entity.ProductId);

			context.Entry(existingEntity).CurrentValues.SetValues(entity);
		}

		public async Task DeleteByIdAsync(long cartId, long productId)
		{
			CartItem entity = await dbSet.SingleAsync(e => e.CartId == cartId && e.ProductId == productId);
			dbSet.Remove(entity);
		}
	}
}
