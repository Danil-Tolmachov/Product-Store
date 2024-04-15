using StoreDAL.Entities;
using StoreDAL.Interfaces.Repositories;

namespace StoreDAL.Infrastructure.Data
{
    public class TestDataFactory : AbstractDataFactory
    {
		public override Contact[] GetContactData()
		{
			return new Contact[]
			{
				new Contact(1) { Name = "Email", Value = "email1@main.com", PersonId = 1 },
				new Contact(2) { Name = "Phone", Value = "+380111111111", PersonId = 1 },
				new Contact(3) { Name = "Phone", Value = "+380222222222", PersonId = 2 },
				new Contact(4) { Name = "Email", Value = "email2@main.com", PersonId = 3 },
				new Contact(5) { Name = "Viber", Value = "+380333333333", PersonId = 4 },
			};
		}

		public override Person[] GetPersonData()
		{
			return new Person[]
			{
				new Person(1) { FirstName = "FirstName1", LastName = "LastName1", Address = "City1, Address1", Discount = 0.1m },
				new Person(2) { FirstName = "FirstName2", LastName = "LastName2", Address = "City2, Address1", Discount = 0.15m },
				new Person(3) { FirstName = "FirstName3", LastName = "LastName3", Address = "City3, Address1", Discount = 0.05m },
				new Person(4) { FirstName = "FirstName4", LastName = "LastName4", Address = "City5, Address1", Discount = 0.05m },
				new Person(5) { FirstName = "FirstName5", LastName = "LastName5", Address = "City1, Address1", Discount = 0.2m },
			};
		}

		public override User[] GetUserData()
		{
			return new User[]
			{
				new User(1) { PersonId = 1, Username = "Username1", Password = "$2a$11$QxP6GG54zUOzEhLs2CORNuNOfokAkM09Op5MFN0oLkTeX3rS/vjsO", IsActive = true },
				new User(2) { PersonId = 2, Username = "Username2", Password = "$2a$11$nVp1qAK9YwOi5LNpFAXfR.SK07.DRBt4KnvIfdpOnahEQO6wJouOy", IsActive = true },
				new User(3) { PersonId = 3, Username = "Username3", Password = "$2a$11$yD69ysc/wPKgIWpLJq2HjOLfDRPymOhlz8jRVoj4sv57L9G.jjSsq", IsActive = true },
				new User(4) { PersonId = 4, Username = "Username4", Password = "$2a$11$sj9d8CY2L0ozSoQfdvce5.TpCiI.dr6iejTOmWBAE1xqJ9rj/wRw6", IsActive = true },
				new User(5) { PersonId = 5, Username = "Username5", Password = "$2a$11$n/bFeF3Xh.QHYwQ1RL6GyuvNe.tytw2NnWlQA0RSEVe6cMFbXRQeu", IsActive = false },
			};
		}

		public override Position[] GetPositionData()
		{
			return new Position[]
			{
				new Position(1) { Name = "Administrator" },
				new Position(2) { Name = "Manager" },
				new Position(3) { Name = "Helper" },
			};
		}

		public override Employee[] GetEmployeeData()
		{
			return new Employee[]
			{
				new Employee(1) { UserId = 4, PositionId = 1 },
				new Employee(2) { UserId = 3, PositionId = 2 },
			};
		}
		
		public override Category[] GetCategoryData()
        {
			return new Category[]
			{
				new Category(1) { Name = "Category1" },
				new Category(2) { Name = "Category2" },
				new Category(3) { Name = "Category3" },
				new Category(4) { Name = "Category4" },
			};
		}

		public override Specification[] GetSpecificationData()
		{
			return new Specification[]
			{
				new Specification(1) { Name = "Weight", Value = "1 kg" },
				new Specification(2) { Name = "Size", Value = "10x12x7" },
				new Specification(3) { Name = "Manufacturer", Value = "Japan" },
			};
		}

		public override Product[] GetProductData()
		{
			return new Product[]
			{
				new Product(1) { 
					Name = "Product1", 
					Description = "Description1", 
					Price = 24.1m, 
					CategoryId = 1, 
					Discount = 0.1m, 
					UnitMeasure = "unit",
				},

				new Product(2) {
					Name = "Product2",
					Description = "Description2",
					Price = 51.4m,
					CategoryId = 2,
					Discount = 0m,
					UnitMeasure = "kg",
				},

				new Product(3) {
					Name = "Product3",
					Description = "Description3",
					Price = 2.4m,
					CategoryId = 4,
					Discount = 0.3m,
					UnitMeasure = "liter",
				},
			};
		}

		public override Order[] GetOrderData()
        {
			return new Order[]
			{
				new Order(1) 
				{ 
					UserId = 1,
					EmployeeId = 1,
					StatusId = 1,
					UserComment = "Comment1",
				},
				new Order(2)
				{
					UserId = 2,
					EmployeeId = 2,
					StatusId = 2,
					UserComment = "Comment2",
				},
				new Order(3)
				{
					UserId = 2,
					EmployeeId = 1,
					StatusId = 3,
					UserComment = "Comment3",
				},
			};
		}

        public override OrderDetail[] GetOrderDetailData()
        {
			return new OrderDetail[]
			{
				new OrderDetail(1) { OrderId = 1, ProductId = 1, Quantity = 2, UnitPrice = 2 },
				new OrderDetail(2) { OrderId = 2, ProductId = 4, Quantity = 1, UnitPrice = 2 },
				new OrderDetail(3) { OrderId = 2, ProductId = 2, Quantity = 5, UnitPrice = 2.3m },
				new OrderDetail(4) { OrderId = 3, ProductId = 1, Quantity = 1, UnitPrice = 5 },
			};
		}

		public override Status[] GetStatusData()
		{
			return new Status[]
			{
				new Status(1) { Name = "Waiting for approve" },
				new Status(2) { Name = "In process" },
				new Status(3) { Name = "Delivered" },
			};
		}

		public override Cart[] GetCartData()
		{
			return new Cart[]
			{
				new Cart(1) { UserId = 1 },
				new Cart(2) { UserId = 2 },
				new Cart(3) { UserId = 3 },
				new Cart(4) { UserId = 4 },
				new Cart(5) { UserId = 5 },
			};
		}

		public override CartItem[] GetCartItemData()
		{
			return Array.Empty<CartItem>();
		}

		public override ProductImage[] GetProductImageData()
		{
			string seedDir = "..\\DataLayer\\Infrastructure\\Data\\SeedImages\\";

			return new ProductImage[]
			{
				new ProductImage(1) { ProductId = 1, Image = File.ReadAllBytes($"{seedDir}\\{1}.webp") },
				new ProductImage(2) { ProductId = 2, Image = File.ReadAllBytes($"{seedDir}\\{2}.webp") },
				new ProductImage(3) { ProductId = 3, Image = File.ReadAllBytes($"{seedDir}\\{3}.webp") },
				new ProductImage(4) { ProductId = 4, Image = File.ReadAllBytes($"{seedDir}\\{4}.webp") },
				new ProductImage(5) { ProductId = 5, Image = File.ReadAllBytes($"{seedDir}\\{5}.webp") },
				new ProductImage(6) { ProductId = 6, Image = File.ReadAllBytes($"{seedDir}\\{6}.webp") },
			};
		}

		public override object[] GetProductSpecificationData()
		{
			return new[]
			{
				new { ProductsId = 1L, SpecificationsId = 1L },
				new { ProductsId = 2L, SpecificationsId = 2L },
				new { ProductsId = 2L, SpecificationsId = 3L },
			};
		}
	}
}
