using StoreBLL.Models;
using StoreDAL.Entities;

namespace StoreBLL.Interfaces.Services
{
	public interface IProductImageService
	{
		Task<byte[]?> GetImageById(long id);

		Task<byte[]?> GetImageByPath(string path);

		Task<IEnumerable<byte[]>> GetImagesByProductId(long productId);
		Task<IEnumerable<ProductImageModel>> GetModelsByProductId(long productId);

		Task<IEnumerable<string>> GetPathesByProductId(long productId);
	}
}
