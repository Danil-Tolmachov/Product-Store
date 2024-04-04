
using AutoMapper;
using StoreBLL.Interfaces.Services;
using StoreBLL.Models;
using StoreBLL.Services.Abstractions;
using StoreDAL.Entities;
using StoreDAL.Interfaces;

namespace StoreBLL.Services
{
	public class UserService : AbstractAdminPanelItem<User, UserModel>, IUserService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public UserService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork.UserRepository)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public Task ChangePassword(long userId, string oldPassword, string newPassword)
		{
			throw new NotImplementedException();
		}

		public async Task<UserModel?> GetByUsername(string username)
		{
			var entity = await _unitOfWork.UserRepository.GetByUsername(username);
			return _mapper.Map<UserModel?>(entity);
		}

		public async Task<UserModel?> Login(string username, string password)
		{
			var entity = await _unitOfWork.UserRepository.Login(username, password);
			return _mapper.Map<UserModel?>(entity);
		}
	}
}
