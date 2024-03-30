using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreDAL.Interfaces;
using StoreDAL.Interfaces.Repositories;
using StoreDAL.Repositories;

namespace StoreDAL.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context = null!;
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
                throw new NotImplementedException();
            }
        }

		public ICartRepository CartRepository
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public ICategoryRepository CategoryRepository
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IContactRepository ContactRepository
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IEmployeeRepository EmployeeRepository
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IOrderDetailRepository OrderDetailRepository
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IOrderRepository OrderRepository
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IPersonRepository PersonRepository
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IPositionRepository PositionRepository
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IProductRepository ProductRepository
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public ISpecificationRepository SpecificationRepository
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IStatusRepository StatusRepository
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IUserRepository UserRepository
		{
			get
			{
				throw new NotImplementedException();
			}
		}


		public UnitOfWork(StoreDbContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this._context = context;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
