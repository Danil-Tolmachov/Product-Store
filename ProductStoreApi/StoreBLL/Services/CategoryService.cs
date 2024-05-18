
using AutoMapper;
using StoreBLL.Interfaces.Services;
using StoreBLL.Models;
using StoreBLL.Services.Abstractions;
using StoreDAL.Entities;
using StoreDAL.Interfaces;

namespace StoreBLL.Services
{
	public class CategoryService : AbstractAdminPanelItem<Category, CategoryModel>, ICategoryService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public CategoryService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork.CategoryRepository)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<ProductModel>> GetProducts(long id)
		{
			var entities = await _unitOfWork.ProductRepository.GetByCategoryId(id);
			return _mapper.Map<IList<ProductModel>>(entities);
		}

		public async Task<IEnumerable<ProductModel>> GetProducts(long id, int pageNumber, int rowCount)
		{
			var entities = await _unitOfWork.ProductRepository.GetByCategoryId(id, pageNumber, rowCount);
			return _mapper.Map<IList<ProductModel>>(entities);
		}
	}
}
