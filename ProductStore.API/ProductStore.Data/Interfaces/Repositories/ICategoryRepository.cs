using StoreDAL.Entities;

namespace StoreDAL.Interfaces.Repositories
{
	public interface ICategoryRepository : ISingleKeyRepository<Category>
	{
		Task<IEnumerable<Category>> GetAllWithProductsAsync();
		Task<IEnumerable<Category>> GetAllWithProductsAsync(int pageNumber, int rowCount);
	}
}
