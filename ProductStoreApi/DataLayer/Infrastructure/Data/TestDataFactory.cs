using StoreDAL.Entities;

namespace StoreDAL.Infrastructure.Data
{
    public class TestDataFactory : AbstractDataFactory
    {
		public override IContactRepository[] GetContactData()
		{
			throw new NotImplementedException();
		}

		public override IPersonRepository[] GetPersonData()
		{
			throw new NotImplementedException();
		}

		public override IUserRepository[] GetUserData()
		{
			throw new NotImplementedException();
		}

		public override IPositionRepository[] GetPositionData()
		{
			throw new NotImplementedException();
		}

		public override IEmployeeRepository[] GetEmployeeData()
		{
			throw new NotImplementedException();
		}
		
		public override ICategoryRepository[] GetCategoryData()
        {
            throw new NotImplementedException();
        }

		public override ISpecificationRepository[] GetSpecificationData()
		{
			throw new NotImplementedException();
		}

		public override IProductRepository[] GetProductData()
		{
			throw new NotImplementedException();
		}

		public override IOrderRepository[] GetOrderData()
        {
			throw new NotImplementedException();
		}
        public override IOrderDetailRepository[] GetOrderDetailData()
        {
			throw new NotImplementedException();
		}
    }
}
