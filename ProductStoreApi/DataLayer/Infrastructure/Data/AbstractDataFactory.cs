using StoreDAL.Entities;

namespace StoreDAL.Infrastructure.Data
{
    public abstract class AbstractDataFactory : IDataFactory
    {
		public abstract Contact[] GetContactData();
		public abstract Person[] GetPersonData();
		public abstract User[] GetUserData();
		public abstract Position[] GetPositionData();
		public abstract Employee[] GetEmployeeData();
		public abstract Category[] GetCategoryData();
		public abstract Specification[] GetSpecificationData();
		public abstract Product[] GetProductData();
		public abstract Order[] GetOrderData();
		public abstract OrderDetail[] GetOrderDetailData();
		public abstract Status[] GetStatusData();
		public abstract Cart[] GetCartData();
		public abstract CartItem[] GetCartItemData();
		public abstract object[] GetProductSpecificationData();
	}
}
