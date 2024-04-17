using Microsoft.EntityFrameworkCore;
using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Interfaces.Repositories;

namespace StoreDAL.Repositories.Repositories
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
