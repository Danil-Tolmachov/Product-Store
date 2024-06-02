
using AutoMapper;
using StoreBLL.Interfaces.Services;
using StoreBLL.Models;
using StoreBLL.Services.Abstractions;
using StoreDAL.Entities;
using StoreDAL.Interfaces;

namespace StoreBLL.Services
{
	public class EmployeeService : AbstractAdminPanelItem<Employee, EmployeeModel>, IEmployeeService
	{
		public EmployeeService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork.EmployeeRepository)
		{
		}
	}
}
