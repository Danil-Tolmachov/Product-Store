
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
		public IConfiguration Config { get; }
		public IWebHostEnvironment Env { get; }

		public Startup(IConfiguration configuration, IWebHostEnvironment env)
		{
			Config = configuration;
			Env = env;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddEndpointsApiExplorer();
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
					options.UseSqlServer(this.Config.GetConnectionString("DockerConnection"));
				}
				else
				{
					options.UseSqlServer(this.Config.GetConnectionString("DefaultConnection"));
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
						var authOptions = new AuthOptions(Config);

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

			// Add API versioning.
			services.AddApiVersioning(options =>
			{
				options.DefaultApiVersion = new ApiVersion(1);
				options.ReportApiVersions = true;
				options.AssumeDefaultVersionWhenUnspecified = true;
				options.ApiVersionReader = new UrlSegmentApiVersionReader();
			}).AddApiExplorer(options =>
			{
				options.GroupNameFormat = "'v'V";
				options.SubstituteApiVersionInUrl = true;
			});

			// Generate swagger docs
			services.AddSwaggerGen((options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "Version 1",
					Title = "ProductStore API V1",
					Description = "Default version of API."
				});
			}));
		}

		public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
		{
			// Configure the HTTP request pipeline.

			// Development environment middlewares.
			if (Env.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI(options =>
				{
					foreach (var groupName in provider.ApiVersionDescriptions.Select(description => description.GroupName))
					{
						options.SwaggerEndpoint(
							$"/swagger/{groupName}/swagger.json", groupName.ToUpperInvariant());
					}
				});
			}
			else
			{
				app.UseHsts();
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
