using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductStoreApi.Authentication;
using ProductStoreApi.Extensions;
using StoreBLL.Interfaces.Services;
using StoreBLL.Models;
using StoreBLL.Models.Dto;
using StoreBLL.Models.Extra;
using System.Security.Claims;

namespace ProductStoreApi.Controllers
{
	[ApiController]
	[Route("/api/auth")]
	public class UserController : ControllerBase
	{
		private readonly ILogger<UserController> _logger;
		private readonly IMapper _mapper;
		private readonly IUserService _userService;

		public UserController(ILogger<UserController> logger, IUserService userService, IMapper mapper)
		{
			_logger = logger;
			_mapper = mapper;
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

		[HttpGet("user")]
		[Authorize]
		[ProducesResponseType(typeof(UserDto), 200)]
		[ProducesResponseType(typeof(string), 401)]
		public async Task<ActionResult<UserDto>> GetUser()
		{
			_logger.LogRequest(nameof(GetUser), HttpContext.Request.Method.ToString());

			try
			{
				string? username = HttpContext.User.FindFirstValue("username");

				if (username is null)
				{
					return Unauthorized("Invalid token.");
				}

				var model = _mapper.Map<UserDto>(await _userService.GetByUsername(username));
				return Ok(model);
			}
			catch (Exception ex)
			{
				_logger.LogException(ex, nameof(GetUser), HttpContext.Request.Method.ToString());
				throw;
			}
		}

		[HttpPost("login")]
		[ProducesResponseType(typeof(object), 200)] // return: JWT Tokens
		[ProducesResponseType(typeof(string), 401)]
		public async Task<ActionResult<TokensDto>> Login([FromBody] LoginModel model)
		{
			_logger.LogRequest(nameof(Login), HttpContext.Request.Method.ToString());

			try
			{
				var user = await _userService.Login(model.Username, model.Password);

				if (user is null)
				{
					return Unauthorized("Invalid credentals.");
				}

				string token = JwtHelper.GetAccessToken(user.Username);
				string refreshToken = JwtHelper.GetRefreshToken(user.Username);

				// Attach refresh token to user
				await _userService.UpdateRefreshToken(user.Username, refreshToken);

				return Ok(new { token, refreshToken });
			}
			catch (Exception ex)
			{
				_logger.LogException(ex, nameof(Login), HttpContext.Request.Method.ToString());
				throw;
			}
		}

		[HttpPost("refresh")]
		[ProducesResponseType(typeof(object), 200)] // return: JWT Tokens
		[ProducesResponseType(typeof(string), 401)]
		public async Task<ActionResult<TokensDto>> Refresh([FromBody] RefreshModel model)
		{
			_logger.LogRequest(nameof(Refresh), HttpContext.Request.Method.ToString());

			try
			{
				if (!ModelState.IsValid)
				{
					return Unauthorized("Token is required.");
				}

				if (!(await JwtHelper.VerifyRefreshToken(model.Token, _userService)))
				{
					return Unauthorized("Invalid refresh token.");
				}

				UserModel user = await JwtHelper.GetUserFromRefresh(model.Token, _userService);

				string token = JwtHelper.GetAccessToken(user.Username);
				string refreshToken = JwtHelper.GetRefreshToken(user.Username);

				// Attach refresh token to user
				await _userService.UpdateRefreshToken(user.Username, refreshToken);

				return Ok(new { token, refreshToken });
			}
			catch (InvalidOperationException ex)
			{
				_logger.LogException(ex, nameof(Refresh), HttpContext.Request.Method.ToString());
				return Unauthorized("Invalid token.");
			}
			catch (ArgumentException ex)
			{
				_logger.LogException(ex, nameof(Refresh), HttpContext.Request.Method.ToString());
				return Unauthorized("Invalid token.");
			}
			catch (Exception ex)
			{
				_logger.LogException(ex, nameof(Refresh), HttpContext.Request.Method.ToString());
				throw;
			}
		}

		[HttpPost("register")]
		[ProducesResponseType(typeof(string), 200)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> Register([FromBody] RegisterModel model)
		{
			_logger.LogRequest(nameof(Register), HttpContext.Request.Method.ToString());

			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				await _userService.Register(model);

				return Ok(new { message = "Registration successful" });
			}
			catch (Exception ex)
			{
				_logger.LogException(ex, nameof(Register), HttpContext.Request.Method.ToString());
				throw;
			}
		}

		[HttpPut("update")]
		[Authorize]
		[ProducesResponseType(typeof(string), 200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		public async Task<IActionResult> UpdateUser(UpdateUserModel model)
		{
			_logger.LogRequest(nameof(UpdateUser), HttpContext.Request.Method.ToString());

			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				string? username = HttpContext.User.FindFirstValue("username");

				if (username is null)
				{
					return Unauthorized();
				}

				var user = await _userService.GetByUsername(username);

				if (user is null)
				{
					return Unauthorized();
				}

				await _userService.UpdateInfo(model, user.Id);

				return Ok(new { message = "Update successful" });
			}
			catch (Exception ex)
			{
				_logger.LogException(ex, nameof(UpdateUser), HttpContext.Request.Method.ToString());
				throw;
			}
		}
	}
}
