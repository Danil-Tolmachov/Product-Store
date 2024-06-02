using ProductStore.Business.Models;
using ProductStore.Data.Entities;

namespace ProductStore.Business.Interfaces.Services
{
	public interface IProductImageService
	{
		Task<byte[]?> GetImageById(long id);

		Task<byte[]?> GetImageByPath(string path);

		Task<IEnumerable<byte[]>> GetImagesByProductId(long productId);
		Task<IEnumerable<ProductImageModel>> GetModelsByProductId(long productId);

		Task<IEnumerable<string>> GetPathsByProductId(long productId);
	}
}
