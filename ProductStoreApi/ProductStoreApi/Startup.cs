
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductStoreApi.Authentication;
using ProductStoreApi.Extensions;
using ProductStoreApi.Filters;
using ProductStoreApi.Middleware;
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

			// Database seed data services.
			services.AddSingleton<IDataFactory, TestDataFactory>();

			// Add Mapper.
			services.AddAutoMapper();

			// Add StoreDbContext.
			services.AddDbContext<StoreDbContext>(options =>
			{
				string? variable = Environment.GetEnvironmentVariable("IsDockerContainer");

				if (bool.TryParse(variable, out bool isDockerContainer) && isDockerContainer)
				{
					options.UseSqlServer(this.Configuration.GetConnectionString("DockerConnection"));
				}
				else
				{
					options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
				}
			});

			// Add Auth services.
			services.AddSingleton<AuthOptions>();
			services.AddSingleton<JwtHelper>();

			// Add PasswordHasher for UserService.
			services.AddSingleton<IPasswordHasher, PasswordHasher>();

			// Add UnitOfWork.
			services.AddTransient<IUnitOfWork, UnitOfWork>();

			// Add data services.
			services.AddStoreServices();

			// Add filters.
			services.AddScoped<FetchUserFilter>();

			// Add JWT.
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
					.AddJwtBearer(options =>
					{
						var authOptions = new AuthOptions(Configuration);

						options.TokenValidationParameters = new TokenValidationParameters
						{
							ValidateIssuer = true,
							ValidIssuer = authOptions.ISSUER,
							ValidateAudience = true,
							ValidAudience = authOptions.AUDIENCE,
							ValidateLifetime = true,
							IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
							ValidateIssuerSigningKey = true,
							ClockSkew = TimeSpan.FromSeconds(60),
						};
					});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			// Configure the HTTP request pipeline.

			// Development environment middlewares.
			if (env.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			// Logging middleware.
			app.UseMiddleware<LoggingRequestMiddleware>();

			// Add Routing Middlewares.
			app.UseHttpsRedirection();
			app.UseRouting();

			app.UseCors(
				options => options.WithOrigins("http://localhost:4200")
								  .AllowAnyMethod()
								  .AllowAnyHeader()
								  .AllowCredentials()
			);

			// Auth middlewares.
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(e =>
				e.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}")
			);

			ApplyDatabaseMigrations(app.ApplicationServices);
		}

		private static void ApplyDatabaseMigrations(IServiceProvider provider)
		{
			using (var scope = provider.CreateScope())
			{
				var services = scope.ServiceProvider;
				var context = services.GetRequiredService<StoreDbContext>();

				context.ApplyMigrations();
			}
		}
	}
}
