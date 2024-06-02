using Microsoft.EntityFrameworkCore;
using ProductStore.Data.Entities;
using ProductStore.Data.Infrastructure;
using ProductStore.Data.Interfaces.Repositories;

namespace ProductStore.Data.Repositories.Repositories
{
	public class ProductImageRepository : AbstractSingleKeyRepository<ProductImage>, IProductImageRepository
	{
		public ProductImageRepository(StoreDbContext context) : base(context)
		{
		}

		public async Task<IEnumerable<ProductImage>> GetAllByProductIdAsync(long productId)
		{
			var entities = dbSet.Where(e => e.ProductId == productId);
			return await entities.ToListAsync();
		}
	}
}
