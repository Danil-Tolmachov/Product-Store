
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using ProductStoreApi.Extensions;
using StoreDAL;
using StoreDAL.Infrastructure;
using StoreDAL.Infrastructure.Data;
using StoreDAL.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using ProductStoreApi.Authentication;

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
			}, ServiceLifetime.Singleton);

			// Add PasswordHasher for UserService
			services.AddSingleton<IPasswordHasher, PasswordHasher>();

			// Add UnitOfWork
			services.AddTransient<IUnitOfWork, UnitOfWork>();

			// Add data services
			services.AddStoreServices();

			// Add JWT
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
					.AddJwtBearer(options =>
					{
						options.TokenValidationParameters = new TokenValidationParameters
						{
							ValidateIssuer = true,
							ValidIssuer = AuthOptions.ISSUER,
							ValidateAudience = true,
							ValidAudience = AuthOptions.AUDIENCE,
							ValidateLifetime = true,
							IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
							ValidateIssuerSigningKey = true,
						};
					});
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
			app.UseAuthentication();
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
