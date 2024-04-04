using StoreBLL.Models;

namespace StoreBLL.Interfaces.Services
{
    public interface IUserService : IAdminPanelItem<UserModel>
    {
		Task<IEnumerable<UserModel>> GetAllWithDetails();
		Task<IEnumerable<UserModel>> GetAllWithDetails(int pageNumber, int rowCount);

		Task<UserModel?> Login(string username, string password);
		Task<UserModel?> GetByUsername(string username);
		Task ChangePassword(long userId, string oldPassword, string newPassword);
	}
}
