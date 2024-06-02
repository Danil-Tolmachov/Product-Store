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
			return await dbSet.Where(p => p.CategoryId == id)
							  .Include(p => p.Category)
							  .Include(p => p.Specifications)
							  .Include(p => p.Images)
							  .AsSplitQuery()
							  .ToListAsync();
		}

		public async Task<IEnumerable<Product>> GetByCategoryId(long id, int pageNumber, int rowCount)
		{
			if (pageNumber < 1)
				throw new ArgumentException("Page number should be greater than or equal to 1.", nameof(pageNumber));

			if (rowCount < 1)
				throw new ArgumentException("Row count should be greater than or equal to 1.", nameof(rowCount));


			int pagesLimit = (int)Math.Ceiling((await dbSet.CountAsync()) / (double)rowCount);

			if (pageNumber > pagesLimit)
			{
				return Enumerable.Empty<Product>();
			}

			int entitiesToSkip = (pageNumber - 1) * rowCount;

			return await dbSet.Where(p => p.CategoryId == id)
							  .Skip(entitiesToSkip)
							  .Take(rowCount)
							  .Include(p => p.Category)
							  .Include(p => p.Specifications)
							  .Include(p => p.Images)
							  .ToListAsync();
		}

		public override async Task<Product> GetByIdAsync(long id)
		{
			try
			{
				return await this.dbSet.Include(p => p.Category)
									   .Include(p => p.Specifications)
									   .Include(p => p.Images)
									   .AsSplitQuery()
									   .SingleAsync(e => e.Id == id);
			}
			catch (InvalidOperationException ex)
			{
				throw new ArgumentException("Provided id does not exist", ex);
			}
		}

		public override async Task<IEnumerable<Product>> GetAllAsync()
		{
			return await dbSet.Include(p => p.Category)
							  .Include(p => p.Specifications)
							  .Include(p => p.Images)
							  .AsSplitQuery()
							  .ToListAsync();
		}

		public override async Task<IEnumerable<Product>> GetAllAsync(int pageNumber, int rowCount)
		{
			if (pageNumber < 1)
				throw new ArgumentException("Page number should be greater than or equal to 1.", nameof(pageNumber));

			if (rowCount < 1)
				throw new ArgumentException("Row count should be greater than or equal to 1.", nameof(rowCount));


			int pagesLimit = (int)Math.Ceiling((await dbSet.CountAsync()) / (double)rowCount);

			if (pageNumber > pagesLimit)
			{
				return Enumerable.Empty<Product>();
			}

			int entitiesToSkip = (pageNumber - 1) * rowCount;

			return await dbSet.Skip(entitiesToSkip)
							  .Take(rowCount)
							  .Include(p => p.Category)
							  .Include(p => p.Specifications)
							  .Include(p => p.Images)
							  .ToListAsync();
		}

		public async Task<int> CountByCategory(long id)
		{
			return await this.dbSet.Where(p => p.CategoryId == id).CountAsync();
		}
	}
}
