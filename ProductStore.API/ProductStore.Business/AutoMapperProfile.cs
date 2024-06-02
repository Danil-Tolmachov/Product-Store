using AutoMapper;
using ProductStore.Business.Models;
using ProductStore.Business.Models.Dto;
using ProductStore.Data.Entities;
using System.Text;

namespace ProductStore.Business
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Contact, ContactModel>()
				.ReverseMap();

			CreateMap<Category, CategoryModel>()
				.ReverseMap();

			CreateMap<Specification, SpecificationModel>();

			CreateMap<Product, ProductModel>()
				.ForMember(pm => pm.Images, p => p.MapFrom(x => x.Images));

			CreateMap<Cart, CartModel>()
				.ForMember(pm => pm.UserId, p => p.MapFrom(x => x.UserId))
				.ForMember(pm => pm.CartItems, p => p.MapFrom(x => x.CartItems))
				.ReverseMap();

			CreateMap<CartItem, CartItemModel>()
				.ForMember(im => im.CartId, i => i.MapFrom(x => x.CartId))
				.ForMember(im => im.ProductId, i => i.MapFrom(x => x.ProductId))
				.ForMember(im => im.CartUserId, i => i.MapFrom(x => x.Cart.UserId))
				.ForMember(im => im.Product, i => i.MapFrom(x => x.Product))
				.ForMember(im => im.ImagePath, i => i.MapFrom(x => ConvertImageIdToPath(x.Product.Images.Select(y => y.Id).FirstOrDefault())))
				.ReverseMap();

			CreateMap<CartItemModel, OrderDetailModel>()
				.ForMember(od => od.OrderId, o => o.Ignore())
				.ForMember(od => od.Order, o => o.Ignore())
				.ForMember(od => od.Product, c => c.MapFrom(x => x.Product))
				.ForMember(od => od.Quantity, o => o.MapFrom(x => x.Quantity))
				.ForMember(od => od.UnitPrice, o => o.MapFrom(x => x.Product.Price - (x.Product.Price * x.Product.Discount)));

			CreateMap<Order, OrderModel>()
				.ForMember(om => om.Status, o => o.MapFrom(x => x.Status.Name))
				.ForMember(om => om.IsCompleted, o => o.MapFrom(x => x.Status.Id == StatusConfiguration.CompletedStatusId))
				.ForMember(om => om.IsCanceled, o => o.MapFrom(x => x.Status.Id == StatusConfiguration.CanceledStatusId))
				.ReverseMap();

			CreateMap<OrderDetail, OrderDetailModel>()
				.ForMember(dm => dm.OrderId, d => d.MapFrom(x => x.Order.Id))
				.ForMember(dm => dm.ProductId, d => d.MapFrom(x => x.Product.Id))
				.ForMember(dm => dm.Product, d => d.MapFrom(x => x.Product))
				.ForMember(dm => dm.Order, d => d.MapFrom(x => x.Order));

			CreateMap<OrderDetailModel, OrderDetail>()
				.ForMember(d => d.Id, dm => dm.MapFrom(x => 0))
				.ForMember(d => d.ProductId, dm => dm.MapFrom(x => x.ProductId))
				.ForMember(d => d.OrderId, dm => dm.MapFrom(x => x.OrderId))
				.ForMember(d => d.Product, dm => dm.Ignore())
				.ForMember(d => d.Order, dm => dm.Ignore());

			CreateMap<Person, PersonModel>()
				.ReverseMap();

			CreateMap<User, UserModel>()
				.ForMember(um => um.FirstName, u => u.MapFrom(x => x.Person.FirstName))
				.ForMember(um => um.LastName, u => u.MapFrom(x => x.Person.LastName))
				.ForMember(um => um.Discount, u => u.MapFrom(x => x.Person.Discount))
				.ForMember(um => um.Address, u => u.MapFrom(x => x.Person.Address))
				.ForMember(um => um.CartId, u => u.MapFrom(x => x.Cart.Id))
				.ForMember(um => um.Cart, u => u.MapFrom(x => x.Cart))
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
				.ForMember(em => em.Product, e => e.MapFrom(x => x.Product))
				.ReverseMap();

			MapDtos();
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

		private void MapDtos()
		{
			CreateMap<SpecificationModel, SpecificationDto>();

			CreateMap<CategoryModel, CategoryDto>();

			CreateMap<CategoryModel, CategoryBriefDto>();

			CreateMap<ProductImageModel, ImageDto>()
				.ForMember(id => id.Path, pm => pm.MapFrom(x => x.ImagePath))
				.ForMember(id => id.Alt, pm => pm.Ignore());

			CreateMap<ProductModel, ProductDto>()
				.ForPath(pd => pd.Category!.Id, p => p.MapFrom(x => x.CategoryId))
				.ForPath(pd => pd.Category!.Name, p => p.MapFrom(x => x.CategoryName))
				.ForMember(pd => pd.Specifications, p => p.MapFrom(x => x.Specifications))
				.ForMember(pd => pd.Images, p => p.MapFrom(x => x.Images))
				.ForMember(pd => pd.Price, p => p.MapFrom(x => Math.Round(x.Price - (x.Price * x.Discount), 2)))
				.ForMember(pd => pd.OriginalPrice, p => p.MapFrom(x => x.Price));

			CreateMap<ContactModel, ContactDto>()
				.ForMember(cd => cd.Type, cm => cm.MapFrom(x => x.Name));

			CreateMap<OrderDetailModel, OrderDetailDto>()
				.ForMember(od => od.Product, om => om.MapFrom(x => x.Product));

			CreateMap<OrderModel, OrderBriefDto>();

			CreateMap<OrderModel, OrderDto>()
				.ForMember(od => od.Total, om => om.MapFrom(x => x.Details.Select(y => y.UnitPrice * y.Quantity).Sum()))
				.ForMember(od => od.Details, om => om.MapFrom(x => x.Details));

			CreateMap<CartItemModel, CartItemDto>()
				.ForMember(cd => cd.Product, ci => ci.MapFrom(x => x.Product));

			CreateMap<CartModel, CartDto>()
				.ForMember(cd => cd.Items, cm => cm.MapFrom(x => x.CartItems))
				.ForMember(cd => cd.Total, cm => cm.MapFrom(x => x.CartItems.Sum(y => y.Product.Price * y.Quantity)));

			CreateMap<UserModel, UserDto>()
				.ForMember(ud => ud.Cart, um => um.MapFrom(x => x.Cart));
		}

		private string ConvertImageIdToPath(long imageId)
		{
			byte[] idBytes = Encoding.UTF8.GetBytes(imageId.ToString());
			string path = Convert.ToBase64String(idBytes);
			return path;
		}
	}
}
