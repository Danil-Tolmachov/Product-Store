using ProductStore.Data.Entities;
using ProductStore.Data.Infrastructure;
using ProductStore.Data.Interfaces.Repositories;

namespace ProductStore.Data.Repositories.Repositories
{
	public class PositionRepository : AbstractSingleKeyRepository<Position>, IPositionRepository
	{
		public PositionRepository(StoreDbContext context) : base(context) { }
	}
}
