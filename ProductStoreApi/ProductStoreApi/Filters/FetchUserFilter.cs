﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using StoreDAL.Infrastructure;
using StoreBLL.Services;
using StoreBLL.Interfaces.Services;
using System.Security.Claims;

namespace ProductStoreApi.Filters
{
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
