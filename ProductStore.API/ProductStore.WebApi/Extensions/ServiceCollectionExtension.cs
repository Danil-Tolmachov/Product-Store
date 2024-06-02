using AutoMapper;
using ProductStore.Business;
using ProductStore.Business.Interfaces.Services;
using ProductStore.Business.Services;

namespace ProductStore.WebApi.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            IMapper mapper = AutoMapperProfile.CreateMapper();

			services.AddSingleton(mapper);
            return services;
        }

        public static IServiceCollection AddStoreServices(this IServiceCollection services)
        {
            services.AddTransient<ICartService, CartService>()
				    .AddTransient<ICategoryService, CategoryService>()
				    .AddTransient<IEmployeeService, EmployeeService>()
					.AddTransient<IProductService, ProductService>()
                    .AddTransient<IUserService, UserService>()
                    .AddTransient<IOrderService, OrderService>()
					.AddTransient<IProductImageService, ProductImageService>();
			return services;
        }
    }
}
