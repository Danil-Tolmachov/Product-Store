using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreDAL.Entities;

namespace StoreDAL.Infrastructure.Data
{
    public interface IDataFactory
    {
		IContactRepository[] GetContactData();
		IPersonRepository[] GetPersonData();
		IUserRepository[] GetUserData();
		IPositionRepository[] GetPositionData();
		IEmployeeRepository[] GetEmployeeData();
		ICategoryRepository[] GetCategoryData();
		ISpecificationRepository[] GetSpecificationData();
		IProductRepository[] GetProductData();
		IOrderRepository[] GetOrderData();
		IOrderDetailRepository[] GetOrderDetailData();
	}
}
