using ProductStore.Data.Entities;

namespace ProductStore.Data.Interfaces.Repositories
{
	public interface IProductImageRepository : ISingleKeyRepository<ProductImage>
	{
		Task<IEnumerable<ProductImage>> GetAllByProductIdAsync(long productId);
	}
}
