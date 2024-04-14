using Microsoft.EntityFrameworkCore;
using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Interfaces;
using StoreDAL.Interfaces.Repositories;

namespace StoreDAL.Repositories.Repositories
{
	public class UserRepository : AbstractSingleKeyRepository<User>, IUserRepository
	{
		private readonly IPasswordHasher _hasher;

		public UserRepository(StoreDbContext context, IPasswordHasher passwordHasher) : base(context)
		{
			_hasher = passwordHasher;
		}


		public override async Task AddAsync(User entity)
		{
			entity.Password = _hasher.HashPassword(entity.Password);
			await this.dbSet.AddAsync(entity);
		}

		public override async Task<User> GetByIdAsync(long id)
		{
			try
			{
				return await this.dbSet.Include(u => u.Cart)
									   .ThenInclude(c => c.CartItems)
									   .ThenInclude(i => i.Product)
									   .ThenInclude(p => p.Category)
									   .Include(u => u.Orders)
									   .ThenInclude(o => o.Status)
									   .Include(u => u.Person)
									   .ThenInclude(p => p.Contacts)
									   .SingleAsync(e => e.Id == id);
			}
			catch (InvalidOperationException ex)
			{
				throw new ArgumentException("Provided id does not exist", ex);
			}
		}

		public override async Task<IEnumerable<User>> GetAllAsync()
		{
			return await dbSet.Include(u => u.Person)
							  .ToListAsync();
		}

		public override async Task<IEnumerable<User>> GetAllAsync(int pageNumber, int rowCount)
		{
			if (pageNumber < 1)
				throw new ArgumentException("Page number should be greater than or equal to 1.", nameof(pageNumber));

			if (rowCount < 1)
				throw new ArgumentException("Row count should be greater than or equal to 1.", nameof(rowCount));


			int pagesLimit = (int)Math.Ceiling((await dbSet.CountAsync()) / (double)rowCount);

			if (pageNumber > pagesLimit)
			{
				return new List<User>();
			}

			int entitiesToSkip = (pageNumber - 1) * rowCount;

			return await dbSet.Skip(entitiesToSkip)
							  .Take(rowCount)
							  .Include(u => u.Person)
							  .ToListAsync();
		}

		public async Task<IEnumerable<User>> GetAllWithDetails()
		{
			return await dbSet.Include(u => u.Cart)
							  .ThenInclude(c => c.CartItems)
							  .ThenInclude(i => i.Product)
							  .ThenInclude(p => p.Category)
							  .Include(u => u.Orders)
							  .ThenInclude(o => o.Status)
							  .Include(u => u.Person)
							  .ThenInclude(p => p.Contacts)
							  .ToListAsync();
		}

		public async Task<IEnumerable<User>> GetAllWithDetails(int pageNumber, int rowCount)
		{
			if (pageNumber < 1)
				throw new ArgumentException("Page number should be greater than or equal to 1.", nameof(pageNumber));

			if (rowCount < 1)
				throw new ArgumentException("Row count should be greater than or equal to 1.", nameof(rowCount));


			int pagesLimit = (int)Math.Ceiling((await dbSet.CountAsync()) / (double)rowCount);

			if (pageNumber > pagesLimit)
			{
				return new List<User>();
			}

			int entitiesToSkip = (pageNumber - 1) * rowCount;

			return await dbSet.Skip(entitiesToSkip)
							  .Take(rowCount)
							  .Include(u => u.Cart)
							  .ThenInclude(c => c.CartItems)
							  .ThenInclude(i => i.Product)
							  .ThenInclude(p => p.Category)
							  .Include(u => u.Orders)
							  .ThenInclude(o => o.Status)
							  .Include(u => u.Person)
							  .ThenInclude(p => p.Contacts)
							  .ToListAsync();
		}


		public async Task<User?> GetByUsername(string username)
		{
			return await dbSet.Include(u => u.Cart)
							  .ThenInclude(c => c.CartItems)
							  .ThenInclude(i => i.Product)
							  .ThenInclude(p => p.Category)
							  .Include(u => u.Orders)
							  .ThenInclude(o => o.Status)
							  .Include(u => u.Person)
							  .ThenInclude(p => p.Contacts)
							  .SingleAsync(u => u.Username == username);
		}

		public async Task<User?> Login(string username, string password)
		{
			User? user = await dbSet.Include(u => u.Cart)
								    .Include(u => u.Person)
								    .ThenInclude(p => p.Contacts)
								    .SingleAsync(u => u.Username == username);

			if (user is null)
			{
				return null;
			}
			var pass5 = _hasher.HashPassword("Password4");

			if (_hasher.VerifyPassword(password, user.Password))
			{
				return user;
			}

			return null;
		}
	}
}
