using Microsoft.EntityFrameworkCore;
using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Interfaces.Repositories;

namespace StoreDAL.Repositories.Repositories
{
	public class ProductRepository : AbstractSingleKeyRepository<Product>, IProductRepository
	{
		public ProductRepository(StoreDbContext context) : base(context) { }

		public async Task<IEnumerable<Product>> GetByCategoryId(long id)
		{
			return await dbSet.Where(p => p.CategoryId == id).ToListAsync();
		}
	}
}
