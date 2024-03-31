using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Repositories;

namespace StoreDAL.Interfaces.Repositories
{
	public class StatusRepository : AbstractRepository<Status>, IStatusRepository
	{
		public StatusRepository(StoreDbContext context) : base(context) { }
	}
}
