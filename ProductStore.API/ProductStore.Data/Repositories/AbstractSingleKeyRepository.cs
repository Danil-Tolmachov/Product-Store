using Microsoft.EntityFrameworkCore;
using ProductStore.Data.Entities;
using ProductStore.Data.Infrastructure;
using ProductStore.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStore.Data.Repositories
{
	public class AbstractSingleKeyRepository<TEntity> : AbstractRepository<TEntity>, ISingleKeyRepository<TEntity> where TEntity : class, IBaseEntity
	{
		public AbstractSingleKeyRepository(StoreDbContext context) : base(context)
		{
		}


		public virtual async Task<TEntity> GetByIdAsync(long id)
		{
			try
			{
				return await this.dbSet
					.SingleAsync(e => e.Id == id);
			}
			catch (InvalidOperationException ex)
			{
				throw new ArgumentException("Provided id does not exist", ex);
			}
		}

		public override async Task Update(TEntity entity)
		{
			var existingEntity = await dbSet.SingleAsync(x => x.Id == entity.Id);

			context.Entry(existingEntity).CurrentValues.SetValues(entity);
		}

		public virtual async Task DeleteByIdAsync(long id)
		{
			TEntity entity = await dbSet.SingleAsync(e => e.Id == id);
			dbSet.Remove(entity);
		}
	}
}
