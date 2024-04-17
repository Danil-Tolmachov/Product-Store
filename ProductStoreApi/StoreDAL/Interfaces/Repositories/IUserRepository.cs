using Microsoft.EntityFrameworkCore;
using StoreDAL.Entities;

namespace StoreDAL.Interfaces.Repositories
{
	public interface IUserRepository : ISingleKeyRepository<User>
	{
		Task<IEnumerable<User>> GetAllWithDetails();
		Task<IEnumerable<User>> GetAllWithDetails(int pageNumber, int rowCount);

		Task<User?> GetByUsername(string username);
		Task<User?> Login(string username, string password);
	}
}
