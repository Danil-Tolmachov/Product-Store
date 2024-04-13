
using AutoMapper;
using StoreBLL.Interfaces.Services;
using StoreBLL.Models;
using StoreBLL.Services.Abstractions;
using StoreDAL.Entities;
using StoreDAL.Interfaces;
using System.Text;

namespace StoreBLL.Services
{
	public class ProductImageService : AbstractCrudService<ProductImage, ProductImageModel>, IProductImageService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public ProductImageService(IUnitOfWork unitOfWork, IMapper mapper) : base(mapper, unitOfWork.ProductImageRepository)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}


		public async Task<byte[]?> GetImageById(long id)
		{
			ProductImage image = await _unitOfWork.ProductImageRepository.GetByIdAsync(id);
			return image.Image;
		}

		public async Task<byte[]?> GetImageByPath(string path)
		{
			long? id = GetImageId(path);

			if (id is null)
			{
				return null;
			}

			ProductImage image = await _unitOfWork.ProductImageRepository.GetByIdAsync((long)id);
			return image.Image;
		}

		public async Task<IEnumerable<byte[]>> GetImagesByProductId(long productId)
		{
			var images = await _unitOfWork.ProductImageRepository.GetAllByProductIdAsync(productId);
			return images.Select(i => i.Image);
		}

		public async Task<IEnumerable<ProductImageModel>> GetModelsByProductId(long productId)
		{
			var images = await _unitOfWork.ProductImageRepository.GetAllByProductIdAsync(productId);
			return _mapper.Map<IList<ProductImageModel>>(images);
		}

		public async Task<IEnumerable<string>> GetPathesByProductId(long productId)
		{
			var images = await _unitOfWork.ProductImageRepository.GetAllByProductIdAsync(productId);
			return images.Select(i => GetImagePath(i.Id));
		}


		private static string GetImagePath(long id)
		{
			byte[] idBytes = Encoding.UTF8.GetBytes(id.ToString());
			string path = Convert.ToBase64String(idBytes);
			return path;
		}

		private static long? GetImageId(string path)
		{
			try
			{
				byte[] idBytes = Convert.FromBase64String(path);
				string idString = Encoding.UTF8.GetString(idBytes);
				return Convert.ToInt64(idString);
			}
			catch
			{
				return null;
			}
		}
	}
}
