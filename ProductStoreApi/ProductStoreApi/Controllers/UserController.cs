using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductStoreApi.Authentication;
using ProductStoreApi.Filters;
using StoreBLL.Interfaces.Services;
using StoreBLL.Models;
using StoreBLL.Models.Dto;
using StoreBLL.Models.Extra;

namespace ProductStoreApi.Controllers
{
	[ApiVersion(1)]
	[ApiController]
	[Route("/api/v{v:apiVersion}/auth")]
	public class UserController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IUserService _userService;
		private readonly JwtHelper _jwtHelper;

		public UserController(IUserService userService, IMapper mapper, JwtHelper jwtHelper)
		{
			_mapper = mapper;
			_userService = userService;
			_jwtHelper = jwtHelper;
		}


		[HttpGet]
		[Authorize]
		[ProducesResponseType(200)]
		[ProducesResponseType(401)]
		[ServiceFilter(typeof(FetchUserFilter))]
		public ActionResult CheckAuthorization()
		{
			return Ok(new { message = "User is Authorized!" });
		}

		[HttpGet("user")]
		[Authorize]
		[ProducesResponseType(typeof(UserDto), 200)]
		[ProducesResponseType(typeof(string), 401)]
		[ServiceFilter(typeof(FetchUserFilter))]
		public ActionResult<UserDto> GetUser()
		{
			UserModel user = (UserModel)HttpContext.Items["User"]!;

			return Ok(_mapper.Map<UserDto>(user));
		}

		[HttpPost("login")]
		[ProducesResponseType(typeof(TokensDto), 200)] // return: JWT Tokens
		[ProducesResponseType(typeof(string), 401)]
		public async Task<ActionResult<TokensDto>> Login([FromBody] LoginModel model)
		{
			var user = await _userService.Login(model.Username, model.Password);

			if (user is null)
			{
				return Unauthorized("Invalid credentals.");
			}

			string token = _jwtHelper.GetAccessToken(user.Username);
			string refreshToken = _jwtHelper.GetRefreshToken(user.Username);

			// Attach refresh token to user
			await _userService.UpdateRefreshToken(user.Username, refreshToken);

			return Ok(new { token, refreshToken });
		}

		[HttpPost("refresh")]
		[ProducesResponseType(typeof(object), 200)] // return: JWT Tokens
		[ProducesResponseType(typeof(string), 401)]
		public async Task<ActionResult<TokensDto>> Refresh([FromBody] RefreshModel model)
		{

			try
			{
				if (!ModelState.IsValid)
				{
					return Unauthorized("Token is required.");
				}

				if (!(await _jwtHelper.VerifyRefreshToken(model.Token, _userService)))
				{
					return Unauthorized("Invalid refresh token.");
				}

				UserModel user = await _jwtHelper.GetUserFromRefresh(model.Token, _userService);

				string token = _jwtHelper.GetAccessToken(user.Username);
				string refreshToken = _jwtHelper.GetRefreshToken(user.Username);

				// Attach refresh token to user
				await _userService.UpdateRefreshToken(user.Username, refreshToken);

				return Ok(new { token, refreshToken });
			}
			catch (InvalidOperationException)
			{
				return Unauthorized("Invalid token.");
			}
			catch (ArgumentException)
			{
				return Unauthorized("Invalid token.");
			}
		}

		[HttpPost("register")]
		[ProducesResponseType(typeof(string), 200)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> Register([FromBody] RegisterModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			await _userService.Register(model);

			return Ok(new { message = "Registration successful" });
		}

		[HttpPut("update")]
		[Authorize]
		[ProducesResponseType(typeof(string), 200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		[ServiceFilter(typeof(FetchUserFilter))]
		public async Task<IActionResult> UpdateUser(UpdateUserModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			UserModel user = (UserModel)HttpContext.Items["User"]!;
			await _userService.UpdateInfo(model, user.Id);

			return Ok(new { message = "Update successful" });
		}
	}
}
