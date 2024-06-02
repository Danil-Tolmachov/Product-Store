
using AutoMapper;
using StoreBLL.Interfaces.Services;
using StoreBLL.Models;
using StoreBLL.Services.Abstractions;
using StoreDAL.Entities;
using StoreDAL.Interfaces;

namespace StoreBLL.Services
{
	/// <summary>
	/// Provides services for managing categories.
	/// </summary>
	public class CategoryService : AbstractAdminPanelItem<Category, CategoryModel>, ICategoryService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public CategoryService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork.CategoryRepository)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		/// <summary>
		/// Gets the products in the specified category.
		/// </summary>
		/// <param name="id">The category identifier.</param>
		/// <returns>An enumerable collection of product models.</returns>
		public async Task<IEnumerable<ProductModel>> GetProducts(long id)
		{
			var entities = await _unitOfWork.ProductRepository.GetByCategoryId(id);
			return _mapper.Map<IList<ProductModel>>(entities);
		}

		/// <summary>
		/// Gets the products in the specified category with pagination.
		/// </summary>
		/// <param name="id">The category identifier.</param>
		/// <param name="pageNumber">The page number.</param>
		/// <param name="rowCount">The number of items per page.</param>
		/// <returns>An enumerable collection of product models.</returns>
		public async Task<IEnumerable<ProductModel>> GetProducts(long id, int pageNumber, int rowCount)
		{
			var entities = await _unitOfWork.ProductRepository.GetByCategoryId(id, pageNumber, rowCount);
			return _mapper.Map<IList<ProductModel>>(entities);
		}
	}
}
