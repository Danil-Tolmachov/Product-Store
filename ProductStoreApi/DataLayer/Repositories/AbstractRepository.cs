using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Interfaces;

namespace StoreDAL.Repositories
{
    public abstract class AbstractRepository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        protected readonly DbSet<TEntity> dbSet;
        protected readonly StoreDbContext context;

        protected AbstractRepository(StoreDbContext context)
        {
            try
            {
                this.context = context;
                this.dbSet = context.Set<TEntity>();
            }
            catch (Exception ex)
            {
                throw new NotSupportedException($"There is no Set<{nameof(TEntity)}> in context.", ex);
            }
        }

        public virtual async Task<int> Count()
        {
            return await this.dbSet.CountAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await this.dbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(int pageNumber, int rowCount)
        {
            if (pageNumber < 1)
                throw new ArgumentException("Page number should be greater than or equal to 1.", nameof(pageNumber));

            if (rowCount < 1)
                throw new ArgumentException("Row count should be greater than or equal to 1.", nameof(rowCount));


            int pagesLimit = (int)Math.Ceiling((await dbSet.CountAsync()) / (double)rowCount);

            if (pageNumber > pagesLimit)
            {
                return Enumerable.Empty<TEntity>();
            }

            int entitiesToSkip = (pageNumber - 1) * rowCount;

            return await dbSet.Skip(entitiesToSkip).Take(rowCount).ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(long id)
        {
            try
            {
                return await this.dbSet
                    .FirstAsync(e => e.Id == id);
            }
            catch (InvalidOperationException ex)
            {
                throw new ArgumentException("Provided id does not exist", ex);
            }
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await this.dbSet.AddAsync(entity);
            await this.context.SaveChangesAsync();
        }

        public virtual void Update(TEntity entity)
        {
            var existingEntity = dbSet.Single(x => x.Id == entity.Id);
            var entityType = typeof(TEntity);
            var properties = entityType.GetProperties();

            foreach (var property in properties)
            {
                if (!property.PropertyType.IsGenericType)
                {
                    var value = property.GetValue(entity);
                    property.SetValue(existingEntity, value);
                }
            }

            context.SaveChanges();
        }


        public virtual void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
            context.SaveChanges();
        }

        public virtual async Task DeleteByIdAsync(long id)
        {
            TEntity entity = await dbSet.FirstAsync(e => e.Id == id);
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
