
using Microsoft.EntityFrameworkCore;
using StoreDAL.Infrastructure.Data;
using StoreDAL.Infrastructure;
using sports_store_application.Extensions;
using StoreDAL.Interfaces;
using StoreDAL;

namespace ProductStoreApi
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
			services.AddControllers();

			// Database seed data services
			services.AddSingleton<IDataFactory, TestDataFactory>();

			// Add Mapper
			services.AddAutoMapper();

			// Add StoreDbContext
			services.AddSingleton<StoreDbContext>(sp =>
			{
				var cf = new StoreDbFactory(new TestDataFactory());
				return cf.CreateDbContext();
			});

			// Add PasswordHasher for UserService
			services.AddSingleton<IPasswordHasher, PasswordHasher>();

			// Add UnitOfWork
			services.AddTransient<IUnitOfWork, UnitOfWork>();

			// Add data services
			services.AddStoreServices();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			// Configure the HTTP request pipeline.
			if (env.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseRouting();
			app.UseAuthorization();

			app.UseEndpoints(e =>
				e.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}")
			);
		}
	}
}
