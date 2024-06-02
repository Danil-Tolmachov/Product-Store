
using AutoMapper;
using ProductStore.Business.Interfaces.Services;
using ProductStore.Business.Models;
using ProductStore.Business.Services.Abstractions;
using ProductStore.Data.Entities;
using ProductStore.Data.Interfaces;

namespace ProductStore.Business.Services
{
	public class EmployeeService : AbstractAdminPanelItem<Employee, EmployeeModel>, IEmployeeService
	{
		public EmployeeService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork.EmployeeRepository)
		{
		}
	}
}
