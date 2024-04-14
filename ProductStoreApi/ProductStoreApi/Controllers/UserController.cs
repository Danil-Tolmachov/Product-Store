using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductStoreApi.Authentication;
using ProductStoreApi.Extensions;
using ProductStoreApi.Models;
using StoreBLL.Interfaces.Services;
using StoreBLL.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProductStoreApi.Controllers
{
	[ApiController]
	[Route("auth")]
	public class UserController : ControllerBase
	{
		private readonly ILogger<UserController> _logger;
		private readonly IUserService _userService;

		public UserController(ILogger<UserController> logger, IUserService userService)
		{
			_logger = logger;
			_userService = userService;
		}


		[HttpGet]
		[Authorize]
		[ProducesResponseType(200)]
		[ProducesResponseType(401)]
		public async Task<ActionResult> CheckAuthorization()
		{
			_logger.LogRequest(nameof(CheckAuthorization), HttpContext.Request.Method.ToString());

			try
			{
				string? username = HttpContext.User.FindFirstValue("username");

				if (username is null)
				{
					return Unauthorized();
				}

				var model = await _userService.GetByUsername(username);

				if (model is null)
				{
					return Unauthorized();
				}

				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogException(ex, nameof(CheckAuthorization), HttpContext.Request.Method.ToString());
				throw;
			}
		}

		[HttpGet]
		[Authorize]
		[Route("user")]
		[ProducesResponseType(typeof(UserModel), 200)]
		[ProducesResponseType(typeof(string), 401)]
		public async Task<ActionResult<UserModel>> GetUser()
		{
			_logger.LogRequest(nameof(GetUser), HttpContext.Request.Method.ToString());

			try
			{
				string? username = HttpContext.User.FindFirstValue("username");

				if (username is null)
				{
					return Unauthorized("Invalid token.");
				}

				var model = await _userService.GetByUsername(username);
				return Ok(model);
			}
			catch (Exception ex)
			{
				_logger.LogException(ex, nameof(GetUser), HttpContext.Request.Method.ToString());
				throw;
			}
		}

		[HttpPost("login")]
		[ProducesResponseType(typeof(object), 200)] // return: JWT Token
		[ProducesResponseType(typeof(string), 401)]
		public async Task<IActionResult> Login([FromBody] LoginModel model)
		{
			try
			{
				var user = await _userService.Login(model.username, model.password);

				if (user is null)
				{
					return Unauthorized("Invalid credentals.");
				}

				var claims = new List<Claim> { new Claim("username", user.Username) };
				var jwt = new JwtSecurityToken(
						issuer: AuthOptions.ISSUER,
						audience: AuthOptions.AUDIENCE,
						claims: claims,
						expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
						signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

				string token = new JwtSecurityTokenHandler().WriteToken(jwt);

				return Ok(new { token });
			}
			catch (Exception ex)
			{
				_logger.LogException(ex, nameof(GetUser), HttpContext.Request.Method.ToString());
				throw;
			}
		}
	}
}
