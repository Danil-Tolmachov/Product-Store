using AutoMapper;
using AutoMapper.Configuration;
using StoreBLL;
using StoreBLL.Interfaces.Services;
using StoreBLL.Services;

namespace ProductStoreApi.Extensions
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
