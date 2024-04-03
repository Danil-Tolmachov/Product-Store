using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Interfaces.Repositories;

namespace StoreDAL.Repositories.Repositories
{
	public class ProductRepository : AbstractSingleKeyRepository<Product>, IProductRepository
	{
		public ProductRepository(StoreDbContext context) : base(context) { }
	}
}
