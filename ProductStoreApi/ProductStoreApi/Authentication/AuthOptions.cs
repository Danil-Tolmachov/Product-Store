using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ProductStoreApi.Authentication
{
	public static class AuthOptions
	{
		public const string ISSUER = "ProductStoreServer";
		public const string AUDIENCE = "ProductStoreClient";
		const string KEY = "This is my temporary secret key, don't use me!!!";
		public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
			new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
	}
}
