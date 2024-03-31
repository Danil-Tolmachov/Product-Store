using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Repositories;

namespace StoreDAL.Interfaces.Repositories
{
	public class EmployeeRepository : AbstractRepository<Employee>, IEmployeeRepository
	{
		public EmployeeRepository(StoreDbContext context) : base(context) { }
	}
}
