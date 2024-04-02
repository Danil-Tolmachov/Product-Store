using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Interfaces.Repositories;

namespace StoreDAL.Repositories.Repositories
{
	public class PositionRepository : AbstractRepository<Position>, IPositionRepository
	{
		public PositionRepository(StoreDbContext context) : base(context) { }
	}
}
