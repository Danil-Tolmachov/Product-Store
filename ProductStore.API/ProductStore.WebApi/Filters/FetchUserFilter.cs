using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProductStore.Business.Interfaces.Services;
using System.Security.Claims;

namespace ProductStore.WebApi.Filters
{
	/// <summary>
	/// Action filter for fetching and attaching user details to the HTTP context.
	/// </summary>
	public class FetchUserFilter : IAsyncActionFilter
	{
		private readonly IUserService _userService;

		public FetchUserFilter(IUserService userService)
		{
			_userService = userService;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			string? username = context.HttpContext.User.FindFirstValue("username");

			if (username is null)
			{
				context.Result = new UnauthorizedResult();
				return;
			}

			var user = await _userService.GetByUsername(username);

			if (user is null)
			{
				context.Result = new UnauthorizedResult();
				return;
			}

			context.HttpContext.Items["User"] = user;

			await next();
		}
	}
}
