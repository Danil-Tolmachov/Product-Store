
using AutoMapper;
using StoreBLL.Interfaces.Services;
using StoreBLL.Models;
using StoreBLL.Services.Abstractions;
using StoreDAL.Entities;
using StoreDAL.Infrastructure;
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

		public async Task<IEnumerable<UserModel>> GetAllWithDetails()
		{
			List<User> entities = (await _unitOfWork.UserRepository.GetAllWithDetails()).ToList();
			return _mapper.Map<IList<UserModel>>(entities);
		}

		public async Task<IEnumerable<UserModel>> GetAllWithDetails(int pageNumber, int rowCount)
		{
			List<User> entities = (await _unitOfWork.UserRepository.GetAllWithDetails(pageNumber, rowCount)).ToList();
			return _mapper.Map<IList<UserModel>>(entities);
		}

		public Task ChangePassword(long userId, string oldPassword, string newPassword)
		{
			throw new NotImplementedException();
		}

		public async Task<UserModel?> GetByUsername(string username)
		{
			try
			{
				var entity = await _unitOfWork.UserRepository.GetByUsername(username);
				return _mapper.Map<UserModel>(entity);
			}
			catch (InvalidOperationException)
			{
				return null;
			}
		}

		public async Task<UserModel?> Login(string username, string password)
		{
			try
			{
				var entity = await _unitOfWork.UserRepository.Login(username, password);
				return _mapper.Map<UserModel>(entity);
			}
			catch (InvalidOperationException)
			{
				return null;
			}
			
		}
	}
}
