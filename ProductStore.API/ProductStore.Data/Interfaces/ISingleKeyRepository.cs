using ProductStore.Data.Entities;

namespace ProductStore.Data.Interfaces
{
    public interface ISingleKeyRepository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntity
	{
		Task<TEntity> GetByIdAsync(long id);
		Task DeleteByIdAsync(long id);
	}
}
