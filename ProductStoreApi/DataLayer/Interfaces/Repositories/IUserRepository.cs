using StoreDAL.Entities;

namespace StoreDAL.Interfaces.Repositories
{
	public interface IUserRepository : IRepository<User>
	{
		Task<User?> GetByUsername(string username);
		Task<User?> Login(string username, string password);
	}
}
