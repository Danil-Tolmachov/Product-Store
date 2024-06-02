
using AutoMapper;
using StoreBLL.Interfaces.Services;
using StoreBLL.Models;
using StoreBLL.Services.Abstractions;
using StoreDAL.Entities;
using StoreDAL.Interfaces;

namespace StoreBLL.Services
{
	/// <summary>
	/// Provides services for managing products.
	/// </summary>
	public class ProductService : AbstractAdminPanelItem<Product, ProductModel>, IProductService
	{
		private readonly IUnitOfWork _unitOfWork;

		public ProductService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork.ProductRepository)
		{
			_unitOfWork = unitOfWork;
		}

		/// <summary>
		/// Gets the count of products by category identifier.
		/// </summary>
		/// <param name="id">The category identifier.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the count of products.</returns>
		public async Task<int> CountByCategory(long id)
		{
			return await this._unitOfWork.ProductRepository.CountByCategory(id);
		}

		/// <summary>
		/// Gets the count of pages by category identifier for pagination purposes.
		/// </summary>
		/// <param name="id">The category identifier.</param>
		/// <param name="rowCount">The number of rows per page. Default is 5.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the count of pages.</returns>
		public async Task<int> CountPagesByCategory(long id, int rowCount = 5)
		{
			var productsCount = await this._unitOfWork.ProductRepository.CountByCategory(id);
			var count = Math.Ceiling(productsCount / (double)rowCount);
			return count > 0 ? (int)count : 1;
		}
	}
}
