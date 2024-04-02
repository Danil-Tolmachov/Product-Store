using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Interfaces.Repositories;

namespace StoreDAL.Repositories.Repositories
{
	public class EmployeeRepository : AbstractRepository<Employee>, IEmployeeRepository
	{
		public EmployeeRepository(StoreDbContext context) : base(context) { }
	}
}
