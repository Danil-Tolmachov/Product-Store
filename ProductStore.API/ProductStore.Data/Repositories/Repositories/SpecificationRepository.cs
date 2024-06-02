using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Interfaces.Repositories;

namespace StoreDAL.Repositories.Repositories
{
	public class SpecificationRepository : AbstractSingleKeyRepository<Specification>, ISpecificationRepository
	{
		public SpecificationRepository(StoreDbContext context) : base(context) { }
	}
}
