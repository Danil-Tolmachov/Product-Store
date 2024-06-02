using Microsoft.EntityFrameworkCore;
using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Interfaces.Repositories;

namespace StoreDAL.Repositories.Repositories
{
	public class OrderDetailRepository : AbstractRepository<OrderDetail>, IOrderDetailRepository
	{
		public OrderDetailRepository(StoreDbContext context) : base(context)
		{

		}

		public async Task<OrderDetail> GetByIdAsync(long orderId, long productId)
		{
			try
			{
				return await dbSet.Include(o => o.Product)
								  .ThenInclude(p => p.Category)
								  .Include(o => o.Order)
								  .AsSplitQuery()
								  .SingleAsync(e => e.ProductId == productId && e.OrderId == orderId);
			}
			catch (InvalidOperationException ex)
			{
				throw new ArgumentException("Item with provided id's does not exist", ex);
			}
		}

		public override async Task Update(OrderDetail entity)
		{
			var existingEntity = await dbSet.SingleAsync(x => x.OrderId == entity.OrderId && x.ProductId == entity.ProductId);

			context.Entry(existingEntity).CurrentValues.SetValues(entity);
		}

		public async Task DeleteByIdAsync(long orderId, long productId)
		{
			OrderDetail entity = await dbSet.SingleAsync(e => e.OrderId == orderId && e.ProductId == productId);
			dbSet.Remove(entity);
		}

		public override async Task<IEnumerable<OrderDetail>> GetAllAsync()
		{
			return await dbSet.Include(o => o.Product)
							  .ThenInclude(p => p.Category)
							  .Include(o => o.Order)
							  .AsSplitQuery()
							  .ToListAsync();
		}

		public override async Task<IEnumerable<OrderDetail>> GetAllAsync(int pageNumber, int rowCount)
		{
			if (pageNumber < 1)
				throw new ArgumentException("Page number should be greater than or equal to 1.", nameof(pageNumber));

			if (rowCount < 1)
				throw new ArgumentException("Row count should be greater than or equal to 1.", nameof(rowCount));


			int pagesLimit = (int)Math.Ceiling((await dbSet.CountAsync()) / (double)rowCount);

			if (pageNumber > pagesLimit)
			{
				return Enumerable.Empty<OrderDetail>();
			}

			int entitiesToSkip = (pageNumber - 1) * rowCount;

			return await dbSet.Skip(entitiesToSkip)
							  .Take(rowCount)
							  .Include(o => o.Product)
							  .ThenInclude(p => p.Category)
							  .Include(o => o.Order)
							  .AsSplitQuery()
							  .ToListAsync();
		}
	}
}
