using Microsoft.EntityFrameworkCore;
using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Interfaces.Repositories;

namespace StoreDAL.Repositories.Repositories
{
	public class EmployeeRepository : AbstractSingleKeyRepository<Employee>, IEmployeeRepository
	{
		public EmployeeRepository(StoreDbContext context) : base(context) { }

		public Task<Employee> GetWithLeastOrders()
		{
			return dbSet.OrderBy(e => e.Orders.Count()).FirstAsync();
		}

		public override async Task<Employee> GetByIdAsync(long id)
		{
			try
			{
				return await this.dbSet.Include(e => e.User)
									   .Include(e => e.User)
									   .ThenInclude(u => u.Person)
									   .Include(e => e.Position)
									   .Include(e => e.Orders)
									   .ThenInclude(o => o.Status)
									   .Include(e => e.Orders)
									   .ThenInclude(o => o.Details)
									   .ThenInclude(d => d.Product)
									   .ThenInclude(p => p.Category)
									   .SingleAsync(e => e.Id == id);
			}
			catch (InvalidOperationException ex)
			{
				throw new ArgumentException("Provided id does not exist", ex);
			}
		}

		public override async Task<IEnumerable<Employee>> GetAllAsync()
		{
			return await dbSet.Include(e => e.User)
							  .Include(e => e.User)
							  .ThenInclude(u => u.Person)
							  .Include(e => e.Position)
							  .ToListAsync();
		}

		public async Task<IEnumerable<Employee>> GetAllWithOrdersAsync()
		{
			return await dbSet.Include(e => e.User)
							  .Include(e => e.User)
							  .ThenInclude(u => u.Person)
							  .Include(e => e.Position)
							  .Include(e => e.Orders)
							  .ThenInclude(o => o.Status)
							  .Include(e => e.Orders)
							  .ThenInclude(o => o.Details)
							  .ThenInclude(d => d.Product)
							  .ThenInclude(p => p.Category)
							  .ToListAsync();
		}

		public override async Task<IEnumerable<Employee>> GetAllAsync(int pageNumber, int rowCount)
		{
			if (pageNumber < 1)
				throw new ArgumentException("Page number should be greater than or equal to 1.", nameof(pageNumber));

			if (rowCount < 1)
				throw new ArgumentException("Row count should be greater than or equal to 1.", nameof(rowCount));


			int pagesLimit = (int)Math.Ceiling((await dbSet.CountAsync()) / (double)rowCount);

			if (pageNumber > pagesLimit)
			{
				return Enumerable.Empty<Employee>();
			}

			int entitiesToSkip = (pageNumber - 1) * rowCount;

			return await dbSet.Skip(entitiesToSkip)
							  .Take(rowCount)
							  .Include(e => e.User)
							  .Include(e => e.User)
							  .ThenInclude(u => u.Person)
							  .Include(e => e.Position)
							  .ToListAsync();
		}

		public async Task<IEnumerable<Employee>> GetAllWithOrdersAsync(int pageNumber, int rowCount)
		{
			if (pageNumber < 1)
				throw new ArgumentException("Page number should be greater than or equal to 1.", nameof(pageNumber));

			if (rowCount < 1)
				throw new ArgumentException("Row count should be greater than or equal to 1.", nameof(rowCount));


			int pagesLimit = (int)Math.Ceiling((await dbSet.CountAsync()) / (double)rowCount);

			if (pageNumber > pagesLimit)
			{
				return Enumerable.Empty<Employee>();
			}

			int entitiesToSkip = (pageNumber - 1) * rowCount;

			return await dbSet.Skip(entitiesToSkip)
							  .Take(rowCount)
							  .Include(e => e.User)
							  .Include(e => e.User)
							  .ThenInclude(u => u.Person)
							  .Include(e => e.Position)
							  .Include(e => e.Orders)
							  .ThenInclude(o => o.Status)
							  .Include(e => e.Orders)
							  .ThenInclude(o => o.Details)
							  .ThenInclude(d => d.Product)
							  .ThenInclude(p => p.Category)
							  .ToListAsync();
		}
	}
}
