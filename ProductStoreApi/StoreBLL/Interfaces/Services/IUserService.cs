using StoreBLL.Models;
using StoreBLL.Models.Extra;

namespace StoreBLL.Interfaces.Services
{
    public interface IUserService : IAdminPanelItem<UserModel>
    {
		Task<IEnumerable<UserModel>> GetAllWithDetails();
		Task<IEnumerable<UserModel>> GetAllWithDetails(int pageNumber, int rowCount);

		Task<UserModel?> Login(string username, string password);
		Task<UserModel?> GetByUsername(string username);

		Task<string?> GetRefreshToken(string username);
		Task UpdateRefreshToken(string username, string token);

		Task<bool> Register(RegisterModel model);
	}
}
