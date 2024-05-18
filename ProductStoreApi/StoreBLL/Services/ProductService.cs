
using AutoMapper;
using StoreBLL.Interfaces.Services;
using StoreBLL.Models;
using StoreBLL.Services.Abstractions;
using StoreDAL.Entities;
using StoreDAL.Interfaces;

namespace StoreBLL.Services
{
	public class ProductService : AbstractAdminPanelItem<Product, ProductModel>, IProductService
	{
		private readonly IUnitOfWork _unitOfWork;

		public ProductService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork.ProductRepository)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<int> CountByCategory(long id)
		{
			return await this._unitOfWork.ProductRepository.CountByCategory(id);
		}

		public async Task<int> CountPagesByCategory(long id, int rowCount = 5)
		{
			var productsCount = await this._unitOfWork.ProductRepository.CountByCategory(id);
			var count = Math.Ceiling(productsCount / (double)rowCount);
			return count > 0 ? (int)count : 1;
		}
	}
}
