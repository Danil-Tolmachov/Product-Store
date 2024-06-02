using ProductStore.Data.Entities;

namespace ProductStore.Data.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(int pageNumber, int rowCount);
        
        Task<int> Count();

        Task AddAsync(TEntity entity);
        void Delete(TEntity entity);
        
        Task Update(TEntity entity);
    }
}
