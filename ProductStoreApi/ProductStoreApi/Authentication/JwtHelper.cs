using Microsoft.IdentityModel.Tokens;
using StoreBLL.Interfaces.Services;
using StoreBLL.Models;
using StoreDAL.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProductStoreApi.Authentication
{
	public static class JwtHelper
	{
		public static string GetAccessToken(string username)
		{
			var claims = new List<Claim> { new Claim("username", username) };
			var jwt = new JwtSecurityToken(
					issuer: AuthOptions.ISSUER,
					audience: AuthOptions.AUDIENCE,
					claims: claims,
					expires: DateTime.UtcNow.Add(AuthOptions.TOKEN_LIFETIME),
					signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

			string token = new JwtSecurityTokenHandler().WriteToken(jwt);
			return token;
		}

		public static string GetRefreshToken(string username)
		{
			var claims = new List<Claim> { new Claim("refresh", username) };
			var jwt = new JwtSecurityToken(
					issuer: AuthOptions.ISSUER,
					audience: AuthOptions.AUDIENCE,
					claims: claims,
					expires: DateTime.UtcNow.Add(AuthOptions.REFRESH_LIFETIME),
					signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

			string token = new JwtSecurityTokenHandler().WriteToken(jwt);
			return token;
		}

		public static async Task<UserModel> GetUser(string jwt, IUserService userService)
		{
			JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
			JwtSecurityToken token = tokenHandler.ReadJwtToken(jwt);

			string? username = token.Claims.FirstOrDefault(c => c.Type == "username")?.Value;

			if (username is null)
			{
				throw new InvalidOperationException("Invalid token.");
			}

			var user = (await userService.GetByUsername(username))
				?? throw new InvalidOperationException("Invalid token.");

			return user;
		}

		public static async Task<UserModel> GetUserFromRefresh(string refresh, IUserService userService)
		{
			JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
			JwtSecurityToken token = tokenHandler.ReadJwtToken(refresh);

			string? username = token.Claims.FirstOrDefault(c => c.Type == "refresh")?.Value;

			if (username is null)
			{
				throw new InvalidOperationException("Invalid token.");
			}

			var user = (await userService.GetByUsername(username))
				?? throw new InvalidOperationException("Invalid token.");

			return user;
		}

		public static async Task<bool> VerifyRefreshToken(string jwt, IUserService userService)
		{
			try
			{
				JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
				JwtSecurityToken token = tokenHandler.ReadJwtToken(jwt);

				if (token.ValidTo < DateTime.UtcNow)
				{
					return false;
				}

				string? username = token.Claims.FirstOrDefault(c => c.Type == "username")?.Value;

				if (string.IsNullOrEmpty(username))
				{
					return false;
				}

				var userModel = await userService.GetByUsername(username);

				if (userModel == null)
				{
					return false;
				}

				string? userRefreshToken = await userService.GetRefreshToken(username);
				if (userRefreshToken != jwt)
				{
					return false;
				}

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
