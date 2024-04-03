using StoreDAL.Entities;

namespace StoreDAL.Interfaces.Repositories
{
	public interface IEmployeeRepository : ISingleKeyRepository<Employee>
	{
		Task<Employee> GetWithLeastOrders();
	}
}
