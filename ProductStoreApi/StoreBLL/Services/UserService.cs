
using AutoMapper;
using StoreBLL.Models.Extra;
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

		public async Task<string?> GetRefreshToken(string username)
		{
			var entity = await _unitOfWork.UserRepository.GetByUsername(username);
			return entity?.RefreshToken;
		}

		public async Task UpdateRefreshToken(string username, string token)
		{
			var entity = await _unitOfWork.UserRepository.GetByUsername(username);

			if (entity is null)
			{
				return;
			}

			entity.RefreshToken = token;
			await _unitOfWork.UserRepository.Update(entity);
			await _unitOfWork.SaveAsync();
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

		public async Task<bool> Register(RegisterModel model)
		{
			try
			{
				// Person 
				Person person = new(0)
				{
					FirstName = model.FirstName,
					LastName = model.LastName,
				};

				// User
				User entity = new(0)
				{
					Username = model.Username,
					Password = model.Password,
					Cart = new Cart(0),
					Person = person,
				};

				// Add address
				if (model.Address is not null)
				{
					entity.Person.Address = model.Address;
				}

				// Add phone number
				if (model.Phone is not null)
				{
					var newList = entity.Person.Contacts.ToList();
					newList.Add(new Contact(0)
					{
						Name = "Phone",
						Value = model.Phone,
						Person = entity.Person,
					});

					entity.Person.Contacts = newList;
				}

				await _unitOfWork.UserRepository.AddAsync(entity);
				await _unitOfWork.SaveAsync();

				return true;
			}
			catch (Exception)
			{
				return false;
			}

		}
	}
}
