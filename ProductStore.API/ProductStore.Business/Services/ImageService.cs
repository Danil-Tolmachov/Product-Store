
using AutoMapper;
using StoreBLL.Interfaces.Services;
using StoreBLL.Models;
using StoreBLL.Services.Abstractions;
using StoreDAL.Entities;
using StoreDAL.Interfaces;
using System.Text;

namespace StoreBLL.Services
{
	/// <summary>
	/// Provides services for managing product images.
	/// </summary>
	public class ProductImageService : AbstractCrudService<ProductImage, ProductImageModel>, IProductImageService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public ProductImageService(IUnitOfWork unitOfWork, IMapper mapper) : base(mapper, unitOfWork.ProductImageRepository)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		/// <summary>
		/// Gets the image bytes by the image identifier.
		/// </summary>
		/// <param name="id">The image identifier.</param>
		/// <returns>The image bytes, or null if not found.</returns>
		public async Task<byte[]?> GetImageById(long id)
		{
			ProductImage image = await _unitOfWork.ProductImageRepository.GetByIdAsync(id);
			return image.Image;
		}

		/// <summary>
		/// Gets the image bytes by the image path.
		/// </summary>
		/// <param name="path">The image path.</param>
		/// <returns>The image bytes, or null if not found.</returns>
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

		/// <summary>
		/// Gets the images bytes for a given product identifier.
		/// </summary>
		/// <param name="productId">The product identifier.</param>
		/// <returns>An enumerable collection of image bytes.</returns>
		public async Task<IEnumerable<byte[]>> GetImagesByProductId(long productId)
		{
			var images = await _unitOfWork.ProductImageRepository.GetAllByProductIdAsync(productId);
			return images.Select(i => i.Image);
		}

		/// <summary>
		/// Gets the image models for a given product identifier.
		/// </summary>
		/// <param name="productId">The product identifier.</param>
		/// <returns>An enumerable collection of product image models.</returns>
		public async Task<IEnumerable<ProductImageModel>> GetModelsByProductId(long productId)
		{
			var images = await _unitOfWork.ProductImageRepository.GetAllByProductIdAsync(productId);
			return _mapper.Map<IList<ProductImageModel>>(images);
		}

		/// <summary>
		/// Gets the image paths for a given product identifier.
		/// </summary>
		/// <param name="productId">The product identifier.</param>
		/// <returns>An enumerable collection of image paths.</returns>
		public async Task<IEnumerable<string>> GetPathsByProductId(long productId)
		{
			var images = await _unitOfWork.ProductImageRepository.GetAllByProductIdAsync(productId);
			return images.Select(i => GetImagePath(i.Id));
		}

		/// <summary>
		/// Converts the image identifier to a base64 encoded path.
		/// </summary>
		/// <param name="id">The image identifier.</param>
		/// <returns>The base64 encoded path.</returns>
		private static string GetImagePath(long id)
		{
			byte[] idBytes = Encoding.UTF8.GetBytes(id.ToString());
			string path = Convert.ToBase64String(idBytes);
			return path;
		}

		/// <summary>
		/// Converts the base64 encoded path to an image identifier.
		/// </summary>
		/// <param name="path">The base64 encoded path.</param>
		/// <returns>The image identifier, or null if the path is invalid.</returns>
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
