using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ProductStore.WebApi.Authentication
{
	public class AuthOptions
	{
		public readonly string ISSUER = string.Empty;
		public readonly string AUDIENCE = string.Empty;

		public readonly TimeSpan TOKEN_LIFETIME = TimeSpan.FromMinutes(8);
		public readonly TimeSpan REFRESH_LIFETIME = TimeSpan.FromDays(30);

		private readonly string KEY = "This is my temporary secret key, don't use me!!!";

		public AuthOptions(IConfiguration configuration)
		{
			IConfigurationSection auth = configuration.GetSection("Auth");

			ISSUER = auth.GetValue<string>("JwtIssuer") ?? string.Empty;
			AUDIENCE = auth.GetValue<string>("JwtAudience") ?? string.Empty;
			KEY = auth.GetValue<string>("SecretKey") ?? string.Empty;

			int TokenLifetimeMinutes = auth.GetValue<int>("TokenLifetime");
			if (TokenLifetimeMinutes > 0)
			{
				TOKEN_LIFETIME = TimeSpan.FromMinutes(TokenLifetimeMinutes);
			}

			int RefreshTokenLifetimeDays = auth.GetValue<int>("RefreshTokenLifetime");
			if (RefreshTokenLifetimeDays > 0)
			{
				REFRESH_LIFETIME = TimeSpan.FromDays(RefreshTokenLifetimeDays);
			}
		}

		public SymmetricSecurityKey GetSymmetricSecurityKey()
		{
			return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
		}
	}
}
