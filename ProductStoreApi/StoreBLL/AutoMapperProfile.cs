using AutoMapper;
using StoreBLL.Models;
using StoreDAL.Entities;

namespace StoreBLL
{
	public class AutomapperProfile : Profile
	{
		public AutomapperProfile()
		{
			CreateMap<Category, CategoryModel>()
				.ReverseMap();

			CreateMap<Specification, SpecificationModel>()
				.ReverseMap();

			CreateMap<Product, ProductModel>()
				.ReverseMap();

			CreateMap<Cart, CartModel>()
				.ReverseMap();

			CreateMap<CartItem, CartItemModel>()
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
