using ProductStore.Data.Entities;

namespace ProductStore.Data.Interfaces.Repositories
{
	public interface IEmployeeRepository : ISingleKeyRepository<Employee>
	{
		Task<Employee> GetWithLeastOrders();
	}
}
