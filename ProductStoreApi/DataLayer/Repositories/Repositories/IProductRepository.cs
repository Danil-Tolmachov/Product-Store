using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Repositories;

namespace StoreDAL.Interfaces.Repositories
{
	public class ProductRepository : AbstractRepository<Product>, IProductRepository
	{
		public ProductRepository(StoreDbContext context) : base(context) { }
	}
}
