using Microsoft.EntityFrameworkCore;
using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Interfaces;
using StoreDAL.Interfaces.Repositories;

namespace StoreDAL.Repositories.Repositories
{
	public class UserRepository : AbstractRepository<User>, IUserRepository
	{
		private readonly IPasswordHasher _hasher;

		public override async Task AddAsync(User entity)
		{
			entity.Password = _hasher.HashPassword(entity.Password);
			await this.dbSet.AddAsync(entity);
		}

		public async Task<User?> GetByUsername(string username)
		{
			return await dbSet.SingleAsync(u => u.Username == username);
		}

		public async Task<User?> Login(string username, string password)
		{
			User? user = await dbSet.SingleAsync(u => u.Username == username);

			if (user is null)
			{
				return null;
			}

			if (_hasher.VerifyPassword(password, user.Password))
			{
				return user;
			}

			return null;
		}

		public UserRepository(StoreDbContext context, IPasswordHasher passwordHasher) : base(context)
		{
			_hasher = passwordHasher;
		}
	}
}
