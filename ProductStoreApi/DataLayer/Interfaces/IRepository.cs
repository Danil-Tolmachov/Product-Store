using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreDAL.Entities;

namespace StoreDAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(int pageNumber, int rowCount);
        Task<TEntity> GetByIdAsync(long id);
        Task<int> Count();

        Task AddAsync(TEntity entity);
        void Delete(TEntity entity);
        Task DeleteByIdAsync(long id);
        void Update(TEntity entity);
    }
}
