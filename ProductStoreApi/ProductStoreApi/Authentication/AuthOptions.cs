using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ProductStoreApi.Authentication
{
	public static class AuthOptions
	{
		public const string ISSUER = "https://localhost:7048/";
		public const string AUDIENCE = "https://localhost:4200/";
		const string KEY = "This is my temporary secret key, don't use me!!!";
		public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
			new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
	}
}
