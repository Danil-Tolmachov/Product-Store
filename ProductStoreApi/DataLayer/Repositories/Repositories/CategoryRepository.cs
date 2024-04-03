using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Interfaces.Repositories;

namespace StoreDAL.Repositories.Repositories
{
	public class CategoryRepository : AbstractSingleKeyRepository<Category>, ICategoryRepository
	{
		public CategoryRepository(StoreDbContext context) : base(context) { }
	}
}
