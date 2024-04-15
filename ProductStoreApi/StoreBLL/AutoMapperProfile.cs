using AutoMapper;
using StoreBLL.Models;
using StoreDAL.Entities;
using System.Text;

namespace StoreBLL
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Contact, ContactModel>()
				.ReverseMap();

			CreateMap<Category, CategoryModel>()
				.ReverseMap();

			CreateMap<IEnumerable<Specification>, Dictionary<string, string>>()
				.ConvertUsing(list => SpecsToDictionary()(list));

			CreateMap<Product, ProductModel>()
				.ForMember(pm => pm.ImagePaths, p => p.MapFrom(x => x.Images.Select(i => ConvertImageIdToPath(i.Id))))
				.PreserveReferences();

			CreateMap<Cart, CartModel>()
				.ForMember(pm => pm.UserId, p => p.MapFrom(x => x.UserId))
				.ForMember(pm => pm.CartItems, p => p.MapFrom(x => x.CartItems))
				.ReverseMap();

			CreateMap<CartItem, CartItemModel>()
				.ForMember(im => im.CartId, i => i.MapFrom(x => x.CartId))
				.ForMember(im => im.ProductId, i => i.MapFrom(x => x.ProductId))
				.ForMember(im => im.CartUserId, i => i.MapFrom(x => x.Cart.UserId))
				.ForMember(im => im.ProductName, i => i.MapFrom(x => x.Product.Name))
				.ForMember(im => im.ProductDescription, i => i.MapFrom(x => x.Product.Description))
				.ForMember(im => im.ProductCategoryId, i => i.MapFrom(x => x.Product.CategoryId))
				.ForMember(im => im.ProductCategoryName, i => i.MapFrom(x => x.Product.Category.Name))
				.ForMember(im => im.ProductDiscount, i => i.MapFrom(x => x.Product.Discount))
				.ForMember(im => im.ProductPrice, i => i.MapFrom(x => x.Product.Price))
				.ReverseMap();

			CreateMap<CartItemModel, OrderDetailModel>()
				.ForMember(od => od.OrderId, o => o.Ignore())
				.ForMember(od => od.ProductName, c => c.MapFrom(x => x.ProductName))
				.ForMember(od => od.ProductDescription, c => c.MapFrom(x => x.ProductDescription))
				.ForMember(od => od.ProductCategoryId, c => c.MapFrom(x => x.ProductCategoryId))
				.ForMember(od => od.ProductCategoryName, c => c.MapFrom(x => x.ProductCategoryName))
				.ForMember(od => od.Quantity, o => o.MapFrom(x => x.Quantity))
				.ForMember(od => od.UnitPrice, o => o.MapFrom(x => x.ProductPrice - (x.ProductPrice * x.ProductDiscount)));

			CreateMap<Order, OrderModel>()
				.ForMember(om => om.Status, o => o.MapFrom(x => x.Status.Name))
				.ReverseMap();

			CreateMap<OrderDetail, OrderDetailModel>()
				.ForMember(dm => dm.OrderId, d => d.MapFrom(x => x.Order.Id))
				.ForMember(dm => dm.ProductId, d => d.MapFrom(x => x.Product.Id))
				.ForMember(dm => dm.ProductName, d => d.MapFrom(x => x.Product.Name))
				.ForMember(dm => dm.ProductDescription, d => d.MapFrom(x => x.Product.Description))
				.ForMember(dm => dm.ProductCategoryId, d => d.MapFrom(x => x.Product.Category.Id))
				.ForMember(dm => dm.ProductCategoryName, d => d.MapFrom(x => x.Product.Category.Name))
				.ForMember(dm => dm.ProductName, d => d.MapFrom(x => x.Product.Name))
				.ReverseMap();

			CreateMap<Person, PersonModel>()
				.ReverseMap();

			CreateMap<User, UserModel>()
				.ForMember(um => um.FirstName, u => u.MapFrom(x => x.Person.FirstName))
				.ForMember(um => um.LastName, u => u.MapFrom(x => x.Person.LastName))
				.ForMember(um => um.Discount, u => u.MapFrom(x => x.Person.Discount))
				.ForMember(um => um.Address, u => u.MapFrom(x => x.Person.Address))
				.ForMember(um => um.CartId, u => u.MapFrom(x => x.Cart.Id))
				.ForMember(um => um.CartItems, u => u.MapFrom(x => x.Cart.CartItems))
				.ForMember(um => um.Contacts, u => u.MapFrom(x => x.Person.Contacts))
				.ForMember(um => um.Password, u => u.Ignore());

			CreateMap<UserModel, User>()
				.ForMember(u => u.Person, um => um.Ignore())
				.ForMember(u => u.Password, um => um.Ignore())
				.ForMember(u => u.IsActive, um => um.MapFrom(x => x.IsActive))
				.ForMember(u => u.Username, um => um.MapFrom(x => x.Username))
				.ForMember(u => u.PersonId, um => um.Ignore())
				.ForMember(u => u.Cart, um => um.Ignore())
				.ForMember(u => u.RefreshToken, um => um.Ignore())
				.ForMember(u => u.Id, um => um.MapFrom(x => x.Id));

			CreateMap<Employee, EmployeeModel>()
				.ForMember(em => em.Username, e => e.MapFrom(x => x.User.Username))
				.ForMember(em => em.FirstName, e => e.MapFrom(x => x.User.Person.FirstName))
				.ForMember(em => em.LastName, e => e.MapFrom(x => x.User.Person.LastName))
				.ForMember(em => em.Position, e => e.MapFrom(x => x.Position.Name))
				.ReverseMap();

			CreateMap<ProductImage, ProductImageModel>()
				.ForMember(em => em.ImagePath, e => e.MapFrom(x => ConvertImageIdToPath(x.Id)))
				.ForMember(em => em.ProductName, e => e.MapFrom(x => x.Product.Name))
				.ReverseMap();
		}

		public static IMapper CreateMapper()
		{
			MapperConfiguration config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<AutoMapperProfile>();
			});

			config.AssertConfigurationIsValid();
			IMapper mapper = config.CreateMapper();

			return mapper;
		}

		private static Func<IEnumerable<Specification>, Dictionary<string, string>> SpecsToDictionary()
		{
			Func<IEnumerable<Specification>, Dictionary<string, string>> func = list =>
			{
                var dict = new Dictionary<string, string>();

				foreach (var item in list)
				{
					dict.Add(item.Name, item.Value);
				}

				return dict;
			};

			return func;
		}

		private string ConvertImageIdToPath(long imageId)
		{
			byte[] idBytes = Encoding.UTF8.GetBytes(imageId.ToString());
			string path = Convert.ToBase64String(idBytes);
			return path;
		}
	}
}
