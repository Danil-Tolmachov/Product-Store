using AutoMapper;
using StoreBLL.Models;
using StoreDAL.Entities;

namespace StoreBLL
{
	public class AutomapperProfile : Profile
	{
		public AutomapperProfile()
		{
			CreateMap<Contact, ContactModel>()
				.ReverseMap();

			CreateMap<Category, CategoryModel>()
				.ReverseMap();

			CreateMap<Specification, SpecificationModel>()
				.ReverseMap();

			CreateMap<Product, ProductModel>()
				.ForMember(pm => pm.Images, p => p.MapFrom(x => x.Images.Select(i => i.Image)))
				.ReverseMap();

			CreateMap<Cart, CartModel>()
				.ReverseMap();

			CreateMap<CartItem, CartItemModel>()
				.ReverseMap();

			CreateMap<CartItemModel, OrderDetailModel>()
				.ForMember(om => om.Product, o => o.MapFrom(x => x.Product))
				.ForMember(om => om.Quantity, o => o.MapFrom(x => x.Quantity))
				.ForMember(om => om.UnitPrice, o => o.MapFrom(x => x.Product.Price - (x.Product.Price * x.Product.Discount)))
				.ReverseMap();

			CreateMap<Order, OrderModel>()
				.ForMember(om => om.Status, o => o.MapFrom(x => x.Status.Name))
				.ReverseMap();

			CreateMap<OrderDetail, OrderDetailModel>()
				.ReverseMap();

			CreateMap<Person, PersonModel>()
				.ReverseMap();

			CreateMap<User, UserModel>()
				.ReverseMap();

			CreateMap<Employee, EmployeeModel>()
				.ForMember(em => em.Position, e => e.MapFrom(x => x.Position.Name))
				.ReverseMap();
		}

		public IMapper CreateMapper()
		{
			MapperConfiguration config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<AutomapperProfile>();
			});

			config.AssertConfigurationIsValid();
			IMapper mapper = config.CreateMapper();

			return mapper;
		}
	}
}
