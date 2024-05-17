using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductStoreApi.Extensions;
using ProductStoreApi.Filters;
using StoreBLL.Interfaces.Services;
using StoreBLL.Models;
using StoreBLL.Models.Dto;
using StoreBLL.Models.Extra;

namespace ProductStoreApi.Controllers
{
	[ApiController]
	[Route("api/order")]
	[ServiceFilter(typeof(FetchUserFilter))]
	public class OrderController : ControllerBase
	{
		private readonly ILogger<OrderController> _logger;
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;

		public OrderController(ILogger<OrderController> logger, IOrderService orderService, IMapper mapper)
		{
			_logger = logger;
			_orderService = orderService;
			_mapper = mapper;
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
				UserModel user = (UserModel)HttpContext.Items["User"]!;

				var model = await _orderService.GetById(orderId);

				if (user.Id == model.UserId)
				{
					return Ok(_mapper.Map<OrderDto>(model));
				}

				return BadRequest();
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
				UserModel user = (UserModel)HttpContext.Items["User"]!;

				var dtos = _mapper.Map<IList<OrderDto>>(await _orderService.GetUserOrders(user.Id));
				return Ok(dtos);
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
				UserModel user = (UserModel)HttpContext.Items["User"]!;

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
				UserModel user = (UserModel)HttpContext.Items["User"]!;

				var orderModel = await _orderService.GetById(model.orderId);

				if (user.Id == orderModel.UserId)
				{
					await _orderService.CancelOrder(model.orderId);
					return Ok();
				}

				return BadRequest();
			}
			catch (Exception ex)
			{
				_logger.LogException(ex, nameof(CancelOrder), HttpContext.Request.Method.ToString());
				throw;
			}
		}
	}
}
