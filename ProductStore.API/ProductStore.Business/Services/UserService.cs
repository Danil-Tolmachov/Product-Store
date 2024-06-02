
using AutoMapper;
using ProductStore.Business.Models.Extra;
using ProductStore.Business.Interfaces.Services;
using ProductStore.Business.Models;
using ProductStore.Business.Services.Abstractions;
using ProductStore.Data.Entities;
using ProductStore.Data.Infrastructure;
using ProductStore.Data.Interfaces;

namespace ProductStore.Business.Services
{
	/// <summary>
	/// Provides services for managing users.
	/// </summary>
	public class UserService : AbstractAdminPanelItem<User, UserModel>, IUserService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public UserService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork.UserRepository)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		/// <summary>
		/// Gets all users with details.
		/// </summary>
		/// <returns>A task that represents the asynchronous operation. The task result contains a list of user models.</returns>
		public async Task<IEnumerable<UserModel>> GetAllWithDetails()
		{
			List<User> entities = (await _unitOfWork.UserRepository.GetAllWithDetails()).ToList();
			return _mapper.Map<IList<UserModel>>(entities);
		}

		/// <summary>
		/// Gets all users with details, with pagination.
		/// </summary>
		/// <param name="pageNumber">The page number.</param>
		/// <param name="rowCount">The number of rows per page.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains a list of user models.</returns>
		public async Task<IEnumerable<UserModel>> GetAllWithDetails(int pageNumber, int rowCount)
		{
			List<User> entities = (await _unitOfWork.UserRepository.GetAllWithDetails(pageNumber, rowCount)).ToList();
			return _mapper.Map<IList<UserModel>>(entities);
		}

		/// <summary>
		/// Changes the password of a user.
		/// </summary>
		/// <param name="userId">The user identifier.</param>
		/// <param name="oldPassword">The old password.</param>
		/// <param name="newPassword">The new password.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		public Task ChangePassword(long userId, string oldPassword, string newPassword)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets a user by username.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the user model or null if not found.</returns>
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

		/// <summary>
		/// Gets the refresh token of a user by username.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the refresh token or null if not found.</returns>
		public async Task<string?> GetRefreshToken(string username)
		{
			var entity = await _unitOfWork.UserRepository.GetByUsername(username);
			return entity?.RefreshToken;
		}

		/// <summary>
		/// Updates the refresh token of a user by username.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="token">The new refresh token.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
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

		/// <summary>
		/// Logs in a user.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the user model or null if login failed.</returns>
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

		/// <summary>
		/// Registers a new user.
		/// </summary>
		/// <param name="model">The registration model.</param>
		/// <returns>A task that represents the asynchronous operation. The task result indicates whether the registration was successful.</returns>
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

		/// <summary>
		/// Updates the information of a user.
		/// </summary>
		/// <param name="model">The update user model.</param>
		/// <param name="userId">The user identifier.</param>
		/// <returns>A task that represents the asynchronous operation. The task result indicates whether the update was successful.</returns>
		public async Task<bool> UpdateInfo(UpdateUserModel model, long userId)
		{
			try
			{
				User user = await _unitOfWork.UserRepository.GetByIdAsync(userId);

				user.Person.FirstName = model.FirstName;
				user.Person.LastName = model.LastName;
				user.Person.Address = model.Address;

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
