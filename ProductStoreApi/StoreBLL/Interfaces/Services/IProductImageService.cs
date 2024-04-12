using StoreBLL.Models;

namespace StoreBLL.Interfaces.Services
{
	public interface IProductImageService
	{
		Task<ProductImageModel?> GetImageById(long id);
		Task<ProductImageModel?> GetImageByPath(string path);
		Task<IEnumerable<ProductImageModel>> GetImagesByProductId(long productId);

		Task<IEnumerable<string>> GetPathesByProductId(long productId);
	}
}
