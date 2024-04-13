﻿
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
			services.AddCors();

			// Database seed data services
			services.AddSingleton<IDataFactory, TestDataFactory>();

			// Add Mapper
			services.AddAutoMapper();

			// Add StoreDbContext
			services.AddDbContext<StoreDbContext>(options =>
			{
				options.UseInMemoryDatabase(Guid.NewGuid().ToString());
			}, ServiceLifetime.Scoped);

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
			app.UseCors(
				options => options.WithOrigins("http://localhost:4200").AllowAnyMethod()
			);

			app.UseEndpoints(e =>
				e.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}")
			);
		}
	}
}
