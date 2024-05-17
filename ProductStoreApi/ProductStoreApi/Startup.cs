
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductStoreApi.Authentication;
using ProductStoreApi.Extensions;
using ProductStoreApi.Filters;
using StoreDAL;
using StoreDAL.Infrastructure;
using StoreDAL.Infrastructure.Data;
using StoreDAL.Interfaces;

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
				options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
			});

			// Add PasswordHasher for UserService
			services.AddSingleton<IPasswordHasher, PasswordHasher>();

			// Add UnitOfWork
			services.AddTransient<IUnitOfWork, UnitOfWork>();

			// Add data services
			services.AddStoreServices();

			// Add filters
			services.AddScoped<FetchUserFilter>();

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
							ClockSkew = TimeSpan.FromSeconds(60),
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

			app.UseCors(
				options => options.WithOrigins("http://localhost:4200")
								  .AllowAnyMethod()
								  .AllowAnyHeader()
								  .AllowCredentials()
			);

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(e =>
				e.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}")
			);
		}
	}
}
