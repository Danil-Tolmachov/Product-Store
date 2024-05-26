using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductStoreApi.Filters;
using StoreBLL.Interfaces.Services;
using StoreBLL.Models;
using StoreBLL.Models.Dto;
using StoreBLL.Models.Extra;

namespace ProductStoreApi.Controllers
{
	[ApiVersion(1)]
	[ApiController]
	[Route("api/v{v:apiVersion}/order")]
	[ServiceFilter(typeof(FetchUserFilter))]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;

		public OrderController(IOrderService orderService, IMapper mapper)
		{
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
			catch (InvalidOperationException)
			{
				return NotFound();
			}
		}

		[HttpGet]
		[Authorize]
		[ProducesResponseType(typeof(IEnumerable<OrderDto>), 200)]
		[ProducesResponseType(401)]
		public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
		{

			try
			{
				UserModel user = (UserModel)HttpContext.Items["User"]!;

				var dtos = _mapper.Map<IList<OrderDto>>(await _orderService.GetUserOrders(user.Id));
				return Ok(dtos);
			}
			catch
			{
				return new List<OrderDto>();
			}
		}

		[HttpPost]
		[Authorize]
		[ProducesResponseType(200)]
		[ProducesResponseType(401)]
		public async Task<ActionResult> SubmitCart(SubmitCartModel submitCartModel)
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

		[HttpPost("cancel")]
		[Authorize]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		public async Task<ActionResult> CancelOrder(CancelOrderModel model)
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
	}
}
