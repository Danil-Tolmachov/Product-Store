using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Repositories;

namespace StoreDAL.Interfaces.Repositories
{
	public class CategoryRepository : AbstractRepository<Category>, ICategoryRepository
	{
		public CategoryRepository(StoreDbContext context) : base(context) { }
	}
}
