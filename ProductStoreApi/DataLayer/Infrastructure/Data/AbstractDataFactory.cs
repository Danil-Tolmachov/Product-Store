using StoreDAL.Entities;

namespace StoreDAL.Infrastructure.Data
{
    public abstract class AbstractDataFactory : IDataFactory
    {
		public abstract IContactRepository[] GetContactData();
		public abstract IPersonRepository[] GetPersonData();
		public abstract IUserRepository[] GetUserData();
		public abstract IPositionRepository[] GetPositionData();
		public abstract IEmployeeRepository[] GetEmployeeData();
		public abstract ICategoryRepository[] GetCategoryData();
		public abstract ISpecificationRepository[] GetSpecificationData();
		public abstract IProductRepository[] GetProductData();
		public abstract IOrderRepository[] GetOrderData();
		public abstract IOrderDetailRepository[] GetOrderDetailData();
	}
}
