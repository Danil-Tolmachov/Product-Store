using ProductStore.Data.Entities;
using ProductStore.Data.Infrastructure;
using ProductStore.Data.Interfaces.Repositories;

namespace ProductStore.Data.Repositories.Repositories
{
	public class SpecificationRepository : AbstractSingleKeyRepository<Specification>, ISpecificationRepository
	{
		public SpecificationRepository(StoreDbContext context) : base(context) { }
	}
}
