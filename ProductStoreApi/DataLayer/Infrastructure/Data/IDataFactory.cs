using StoreDAL.Interfaces.Repositories;

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
