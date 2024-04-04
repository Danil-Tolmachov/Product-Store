using StoreDAL.Interfaces;
using StoreDAL.Interfaces.Repositories;
using StoreDAL.Repositories.Repositories;

namespace StoreDAL.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
		private readonly IPasswordHasher _hasher;

        private ICartItemRepository cartItemRepository = null!;
		private ICartRepository cartRepository = null!;
		private ICategoryRepository categoryRepository = null!;
		private IContactRepository contactRepository = null!;
		private IEmployeeRepository employeeRepository = null!;
		private IOrderDetailRepository orderDetailRepository = null!;
		private IOrderRepository orderRepository = null!;
        private IPersonRepository personRepository = null!;
		private IPositionRepository positionRepository = null!;
		private IProductRepository productRepository = null!;
		private ISpecificationRepository specificationRepository = null!;
		private IStatusRepository statusRepository = null!;
		private IUserRepository userRepository = null!;


		public ICartItemRepository CartItemRepository
        {
            get
            {
				cartItemRepository ??= new CartItemRepository(_context);

				return cartItemRepository;
            }
        }

		public ICartRepository CartRepository
		{
			get
			{
				cartRepository ??= new CartRepository(_context);

				return cartRepository;
			}
		}

		public ICategoryRepository CategoryRepository
		{
			get
			{
				categoryRepository ??= new CategoryRepository(_context);

				return categoryRepository;
			}
		}

		public IContactRepository ContactRepository
		{
			get
			{
				contactRepository ??= new ContactRepository(_context);

				return contactRepository;
			}
		}

		public IEmployeeRepository EmployeeRepository
		{
			get
			{
				employeeRepository ??= new EmployeeRepository(_context);

				return employeeRepository;
			}
		}

		public IOrderDetailRepository OrderDetailRepository
		{
			get
			{
				orderDetailRepository ??= new OrderDetailRepository(_context);

				return orderDetailRepository;
			}
		}

		public IOrderRepository OrderRepository
		{
			get
			{
				orderRepository ??= new OrderRepository(_context);

				return orderRepository;
			}
		}

		public IPersonRepository PersonRepository
		{
			get
			{
				personRepository ??= new PersonRepository(_context);

				return personRepository;
			}
		}

		public IPositionRepository PositionRepository
		{
			get
			{
				positionRepository ??= new PositionRepository(_context);

				return positionRepository;
			}
		}

		public IProductRepository ProductRepository
		{
			get
			{
				productRepository ??= new ProductRepository(_context);

				return productRepository;
			}
		}

		public ISpecificationRepository SpecificationRepository
		{
			get
			{
				specificationRepository ??= new SpecificationRepository(_context);

				return specificationRepository;
			}
		}

		public IStatusRepository StatusRepository
		{
			get
			{
				statusRepository ??= new StatusRepository(_context);

				return statusRepository;
			}
		}

		public IUserRepository UserRepository
		{
			get
			{
				userRepository ??= new UserRepository(_context, _hasher);

				return userRepository;
			}
		}


		public UnitOfWork(StoreDbContext context, IPasswordHasher passwordHasher)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this._context = context;
			this._hasher = passwordHasher;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
