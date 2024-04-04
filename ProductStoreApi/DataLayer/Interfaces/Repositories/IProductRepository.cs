using StoreDAL.Entities;

namespace StoreDAL.Interfaces.Repositories
{
	public interface IProductRepository : ISingleKeyRepository<Product>
	{
		Task<IEnumerable<Product>> GetByCategoryId(long id);
	}
}
