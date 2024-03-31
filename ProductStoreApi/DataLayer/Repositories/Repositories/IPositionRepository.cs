using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Repositories;

namespace StoreDAL.Interfaces.Repositories
{
	public class PositionRepository : AbstractRepository<Position>, IPositionRepository
	{
		public PositionRepository(StoreDbContext context) : base(context) { }
	}
}
