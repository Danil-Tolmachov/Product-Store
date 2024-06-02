using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Interfaces.Repositories;

namespace StoreDAL.Repositories.Repositories
{
	public class StatusRepository : AbstractSingleKeyRepository<Status>, IStatusRepository
	{
		public StatusRepository(StoreDbContext context) : base(context) { }
	}
}
