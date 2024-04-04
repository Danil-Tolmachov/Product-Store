using StoreBLL.Models;

namespace StoreBLL.Interfaces.Services
{
    public interface IUserService : IAdminPanelItem<UserModel>
    {
        Task<UserModel?> Login(string username, string password);
		Task<UserModel?> GetByUsername(string username);
		Task ChangePassword(long userId, string oldPassword, string newPassword);
	}
}
