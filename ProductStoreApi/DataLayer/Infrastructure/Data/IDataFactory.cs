using StoreDAL.Entities;

namespace StoreDAL.Infrastructure.Data
{
    public interface IDataFactory
    {
		Contact[] GetContactData();
		Person[] GetPersonData();
		User[] GetUserData();
		Position[] GetPositionData();
		Employee[] GetEmployeeData();
		Category[] GetCategoryData();
		Specification[] GetSpecificationData();
		Product[] GetProductData();
		Order[] GetOrderData();
		OrderDetail[] GetOrderDetailData();
		Status[] GetStatusData();
		Cart[] GetCartData();
		CartItem[] GetCartItemData();

		object[] GetProductSpecificationData();
	}
}
