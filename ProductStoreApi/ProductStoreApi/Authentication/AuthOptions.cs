using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ProductStoreApi.Authentication
{
	public static class AuthOptions
	{
		public const string ISSUER = "https://localhost:7048/";
		public const string AUDIENCE = "https://localhost:4200/";

		public static readonly TimeSpan TOKEN_LIFETIME = TimeSpan.FromMinutes(10);
		public static readonly TimeSpan REFRESH_LIFETIME = TimeSpan.FromDays(30);

		const string KEY = "This is my temporary secret key, don't use me!!!";
		public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
			new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
	}
}
