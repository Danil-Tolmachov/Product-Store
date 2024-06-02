using ProductStore.Data.Interfaces.Repositories;

namespace ProductStore.Data.Interfaces
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
