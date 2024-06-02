using StoreDAL.Entities;

namespace StoreDAL.Interfaces.Repositories
{
	public interface IProductRepository : ISingleKeyRepository<Product>
	{
		Task<IEnumerable<Product>> GetByCategoryId(long id);
		Task<IEnumerable<Product>> GetByCategoryId(long id, int pageNumber, int rowCount);

		Task<int> CountByCategory(long id);
	}
}
