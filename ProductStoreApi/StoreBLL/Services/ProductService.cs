
using AutoMapper;
using StoreBLL.Interfaces.Services;
using StoreBLL.Models;
using StoreBLL.Services.Abstractions;
using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Interfaces;

namespace StoreBLL.Services
{
	public class ProductService : AbstractAdminPanelItem<Product, ProductModel>, IProductService
	{
		public ProductService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork.ProductRepository)
		{
		}
	}
}
