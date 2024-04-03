using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Interfaces.Repositories;

namespace StoreDAL.Repositories.Repositories
{
	public class PositionRepository : AbstractSingleKeyRepository<Position>, IPositionRepository
	{
		public PositionRepository(StoreDbContext context) : base(context) { }
	}
}
