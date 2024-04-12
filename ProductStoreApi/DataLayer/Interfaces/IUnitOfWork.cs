using StoreDAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDAL.Interfaces
{
    public interface IUnitOfWork
    {
		ICartItemRepository CartItemRepository { get; }
		ICartRepository CartRepository { get; }
		ICategoryRepository CategoryRepository { get; }
		IContactRepository ContactRepository { get; }
		IEmployeeRepository EmployeeRepository { get; }
		IOrderDetailRepository OrderDetailRepository { get; }
		IOrderRepository OrderRepository { get; }
		IPersonRepository PersonRepository { get; }
		IPositionRepository PositionRepository { get; }
		IProductRepository ProductRepository { get; }
		ISpecificationRepository SpecificationRepository { get; }
		IStatusRepository StatusRepository { get; }
		IUserRepository UserRepository { get; }
		IProductImageRepository ProductImageRepository { get; }

		Task SaveAsync();
    }
}
