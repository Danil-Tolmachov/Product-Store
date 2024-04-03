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
				return await dbSet
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
	}
}
