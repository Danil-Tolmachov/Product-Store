using Microsoft.EntityFrameworkCore;
using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Interfaces.Repositories;

namespace StoreDAL.Repositories.Repositories
{
	public class OrderRepository : AbstractSingleKeyRepository<Order>, IOrderRepository
	{
		public OrderRepository(StoreDbContext context) : base(context) { }

		public async Task<IEnumerable<Order>> GetByUser(long userId)
		{
			return await dbSet.Where(o => o.UserId == userId)
							  .Include(o => o.User)
							  .Include(o => o.Employee)
							  .Include(o => o.Details)
							  .ThenInclude(d => d.Product)
							  .ThenInclude(p => p.Category)
							  .Include(o => o.Status)
							  .AsSplitQuery()
							  .ToListAsync();
		}

		public override async Task<Order> GetByIdAsync(long id)
		{
			try
			{
				return await this.dbSet.Include(o => o.User)
									   .Include(o => o.Employee)
									   .Include(o => o.Details)
									   .ThenInclude(d => d.Product)
									   .ThenInclude(p => p.Category)
									   .Include(o => o.Status)
									   .AsSplitQuery()
									   .SingleAsync(e => e.Id == id);
			}
			catch (InvalidOperationException ex)
			{
				throw new ArgumentException("Provided id does not exist", ex);
			}
		}

		public override async Task<IEnumerable<Order>> GetAllAsync()
		{
			return await dbSet.Include(o => o.User)
							  .Include(o => o.Employee)
							  .Include(o => o.Details)
							  .ThenInclude(d => d.Product)
							  .ThenInclude(p => p.Category)
							  .Include(o => o.Status)
							  .AsSplitQuery()
							  .ToListAsync();
		}

		public override async Task<IEnumerable<Order>> GetAllAsync(int pageNumber, int rowCount)
		{
			if (pageNumber < 1)
				throw new ArgumentException("Page number should be greater than or equal to 1.", nameof(pageNumber));

			if (rowCount < 1)
				throw new ArgumentException("Row count should be greater than or equal to 1.", nameof(rowCount));


			int pagesLimit = (int)Math.Ceiling((await dbSet.CountAsync()) / (double)rowCount);

			if (pageNumber > pagesLimit)
			{
				return Enumerable.Empty<Order>();
			}

			int entitiesToSkip = (pageNumber - 1) * rowCount;

			return await dbSet.Skip(entitiesToSkip)
							  .Take(rowCount)
							  .Include(o => o.User)
							  .Include(o => o.Employee)
							  .Include(o => o.Details)
							  .ThenInclude(d => d.Product)
							  .ThenInclude(p => p.Category)
							  .Include(o => o.Status)
							  .AsSplitQuery()
							  .ToListAsync();
		}
	}
}
