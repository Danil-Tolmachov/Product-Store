using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Repositories;

namespace StoreDAL.Interfaces.Repositories
{
	public class SpecificationRepository : AbstractRepository<Specification>, ISpecificationRepository
	{
		public SpecificationRepository(StoreDbContext context) : base(context) { }
	}
}
