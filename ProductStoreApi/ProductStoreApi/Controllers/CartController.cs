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
	[Route("api/cart")]
	[ServiceFilter(typeof(FetchUserFilter))]
	public class CartController : ControllerBase
	{
		private readonly ILogger<CartController> _logger;
		private readonly ICartService _cartService;
		private readonly IMapper _mapper;

		public CartController(ILogger<CartController> logger, ICartService cartService, IMapper mapper)
		{
			_logger = logger;
			_cartService = cartService;
			_mapper = mapper;
		}

		[HttpGet]
		[Authorize]
		[ProducesResponseType(typeof(CartDto), 200)]
		[ProducesResponseType(401)]
		public async Task<ActionResult<CartDto>> GetCart()
		{
			_logger.LogRequest(nameof(GetCart), HttpContext.Request.Method.ToString());

			try
			{
				UserModel user = (UserModel)HttpContext.Items["User"]!;

				var models = _mapper.Map<CartDto>(await _cartService.GetUserCart(user.Id));
				return Ok(models);
			}
			catch (Exception ex)
			{
				_logger.LogException(ex, nameof(GetCart), HttpContext.Request.Method.ToString());
				throw;
			}
		}

		[HttpPost]
		[Authorize]
		[ProducesResponseType(200)]
		[ProducesResponseType(typeof(string), 400)]
		[ProducesResponseType(401)]
		public async Task<IActionResult> AddToCart(AddCartItemModel requestModel)
		{
			_logger.LogRequest(nameof(AddToCart), HttpContext.Request.Method.ToString());

			try
			{
				UserModel user = (UserModel)HttpContext.Items["User"]!;

				var cartItemModel = new CartItemModel()
				{
					Quantity = requestModel.Quantity,
					ProductId = requestModel.ProductId,
					CartId = user.Cart.Id
				};

				await _cartService.AddProduct(cartItemModel, user.Id);
				return Ok();
			}
			catch (ArgumentException ex)
			{
				_logger.LogException(ex, nameof(AddToCart), HttpContext.Request.Method.ToString());
				return BadRequest("Invalid input.");
			}
			catch (Exception ex)
			{
				_logger.LogException(ex, nameof(AddToCart), HttpContext.Request.Method.ToString());
				throw;
			}
		}

		[HttpDelete]
		[Authorize]
		[ProducesResponseType(200)]
		[ProducesResponseType(401)]
		public async Task<IActionResult> ClearCart()
		{
			_logger.LogRequest(nameof(AddToCart), HttpContext.Request.Method.ToString());

			try
			{
				UserModel user = (UserModel)HttpContext.Items["User"]!;

				await _cartService.ClearUserCart(user.Id);
				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogException(ex, nameof(AddToCart), HttpContext.Request.Method.ToString());
				throw;
			}
		}

		[HttpDelete("{id}")]
		[Authorize]
		[ProducesResponseType(200)]
		[ProducesResponseType(typeof(string), 400)]
		[ProducesResponseType(401)]
		public async Task<IActionResult> RemoveFromCart(long id)
		{
			_logger.LogRequest(nameof(AddToCart), HttpContext.Request.Method.ToString());

			try
			{
				UserModel user = (UserModel)HttpContext.Items["User"]!;

				var productModel = new ProductModel()
				{
					Id = id,
				};

				await _cartService.RemoveProduct(productModel, user.Id);
				return Ok();
			}
			catch (ArgumentException ex)
			{
				_logger.LogException(ex, nameof(AddToCart), HttpContext.Request.Method.ToString());
				return BadRequest("Invalid id was provided.");
			}
			catch (Exception ex)
			{
				_logger.LogException(ex, nameof(AddToCart), HttpContext.Request.Method.ToString());
				throw;
			}
		}
	}
}
