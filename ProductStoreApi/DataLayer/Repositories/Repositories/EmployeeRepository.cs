using Microsoft.EntityFrameworkCore;
using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Interfaces.Repositories;

namespace StoreDAL.Repositories.Repositories
{
	public class EmployeeRepository : AbstractSingleKeyRepository<Employee>, IEmployeeRepository
	{
		public EmployeeRepository(StoreDbContext context) : base(context) { }

		public Task<Employee> GetWithLeastOrders()
		{
			return dbSet.OrderBy(e => e.Orders.Count()).FirstAsync();
		}
	}
}
