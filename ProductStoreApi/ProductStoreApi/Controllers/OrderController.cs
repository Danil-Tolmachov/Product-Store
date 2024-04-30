using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductStoreApi.Extensions;
using StoreBLL.Interfaces.Services;
using StoreBLL.Models.Dto;
using StoreBLL.Models.Extra;
using System.Security.Claims;

namespace ProductStoreApi.Controllers
{
	[ApiController]
	[Route("api/order")]
	public class OrderController : ControllerBase
	{
		private readonly ILogger<OrderController> _logger;
		private readonly IOrderService _orderService;
		private readonly IUserService _userService;
		private readonly IMapper _mapper;

		public OrderController(ILogger<OrderController> logger, IOrderService orderService, IMapper mapper, IUserService userService)
		{
			_logger = logger;
			_orderService = orderService;
			_mapper = mapper;
			_userService = userService;
		}


		[HttpGet("{id}")]
		[Authorize]
		[ProducesResponseType(typeof(OrderDto), 200)]
		[ProducesResponseType(401)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<OrderDto>> GetOrder(long orderId)
		{
			_logger.LogRequest(nameof(GetOrder), HttpContext.Request.Method.ToString());

			try
			{
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

				var model = _mapper.Map<OrderDto>(await _orderService.GetById(orderId));
				return Ok(model);
			}
			catch (InvalidOperationException ex)
			{
				_logger.LogException(ex, nameof(GetOrder), HttpContext.Request.Method.ToString());
				return NotFound();
			}
			catch (Exception ex)
			{
				_logger.LogException(ex, nameof(GetOrder), HttpContext.Request.Method.ToString());
				throw;
			}
		}

		[HttpGet]
		[Authorize]
		[ProducesResponseType(typeof(IEnumerable<OrderDto>), 200)]
		[ProducesResponseType(401)]
		public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
		{
			_logger.LogRequest(nameof(GetOrders), HttpContext.Request.Method.ToString());

			try
			{
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

				var models = _mapper.Map<IList<OrderDto>>(await _orderService.GetUserOrders(user.Id));
				return Ok(models);
			}
			catch (Exception ex)
			{
				_logger.LogException(ex, nameof(GetOrders), HttpContext.Request.Method.ToString());
				return new List<OrderDto>();
			}
		}

		[HttpPost]
		[Authorize]
		[ProducesResponseType(200)]
		[ProducesResponseType(401)]
		public async Task<ActionResult> SubmitCart(SubmitCartModel submitCartModel)
		{
			_logger.LogRequest(nameof(SubmitCart), HttpContext.Request.Method.ToString());

			try
			{
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

				// Create Order
				SubmitOrderModel model = new()
				{
					UserId = user.Id,
					UserComment = submitCartModel.Comment,
					CartItems = user.Cart.CartItems,
				};

				await _orderService.SubmitCart(model);
				await _orderService.ClearCart(user.Id);

				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogException(ex, nameof(SubmitCart), HttpContext.Request.Method.ToString());
				throw;
			}
		}

		[HttpPost("cancel")]
		[Authorize]
		[ProducesResponseType(200)]
		[ProducesResponseType(401)]
		public async Task<ActionResult> CancelOrder(CancelOrderModel model)
		{
			_logger.LogRequest(nameof(CancelOrder), HttpContext.Request.Method.ToString());

			try
			{
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

				await _orderService.CancelOrder(model.orderId);

				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogException(ex, nameof(CancelOrder), HttpContext.Request.Method.ToString());
				throw;
			}
		}
	}
}
