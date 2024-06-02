using ProductStore.Data.Entities;
using ProductStore.Data.Infrastructure;
using ProductStore.Data.Interfaces.Repositories;

namespace ProductStore.Data.Repositories.Repositories
{
	public class StatusRepository : AbstractSingleKeyRepository<Status>, IStatusRepository
	{
		public StatusRepository(StoreDbContext context) : base(context) { }
	}
}
