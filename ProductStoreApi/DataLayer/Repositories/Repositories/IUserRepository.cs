using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Repositories;

namespace StoreDAL.Interfaces.Repositories
{
	public class UserRepository : AbstractRepository<User>, IUserRepository
	{
		public UserRepository(StoreDbContext context) : base(context) { }
	}
}
