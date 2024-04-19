using Microsoft.EntityFrameworkCore;
using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Interfaces.Repositories;

namespace StoreDAL.Repositories.Repositories
{
	public class CategoryRepository : AbstractSingleKeyRepository<Category>, ICategoryRepository
	{
		public CategoryRepository(StoreDbContext context) : base(context) { }


		public override async Task<Category> GetByIdAsync(long id)
		{
			try
			{
				return await this.dbSet.Include(c => c.Products)
								       .ThenInclude(p => p.Images)
									   .AsSplitQuery()
									   .SingleAsync(e => e.Id == id);
			}
			catch (InvalidOperationException ex)
			{
				throw new ArgumentException("Provided id does not exist", ex);
			}
		}

		public async Task<IEnumerable<Category>> GetAllWithProductsAsync()
		{
			return await this.dbSet.Include(c => c.Products)
								   .ThenInclude(p => p.Images)
								   .AsSplitQuery()
								   .ToListAsync();
		}

		public async Task<IEnumerable<Category>> GetAllWithProductsAsync(int pageNumber, int rowCount)
		{
			if (pageNumber < 1)
				throw new ArgumentException("Page number should be greater than or equal to 1.", nameof(pageNumber));

			if (rowCount < 1)
				throw new ArgumentException("Row count should be greater than or equal to 1.", nameof(rowCount));


			int pagesLimit = (int)Math.Ceiling((await dbSet.CountAsync()) / (double)rowCount);

			if (pageNumber > pagesLimit)
			{
				return Enumerable.Empty<Category>();
			}

			int entitiesToSkip = (pageNumber - 1) * rowCount;

			return await dbSet.Skip(entitiesToSkip)
						      .Take(rowCount)
						      .Include(c => c.Products)
							  .ThenInclude(p => p.Images)
							  .AsSplitQuery()
							  .ToListAsync();
		}
	}
}
