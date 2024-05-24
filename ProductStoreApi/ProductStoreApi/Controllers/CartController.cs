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
	[ApiController]
	[Route("api/cart")]
	[ServiceFilter(typeof(FetchUserFilter))]
	public class CartController : ControllerBase
	{
		private readonly ICartService _cartService;
		private readonly IMapper _mapper;

		public CartController(ICartService cartService, IMapper mapper)
		{
			_cartService = cartService;
			_mapper = mapper;
		}

		[HttpGet]
		[Authorize]
		[ProducesResponseType(typeof(CartDto), 200)]
		[ProducesResponseType(401)]
		public async Task<ActionResult<CartDto>> GetCart()
		{
			UserModel user = (UserModel)HttpContext.Items["User"]!;

			var models = _mapper.Map<CartDto>(await _cartService.GetUserCart(user.Id));
			return Ok(models);
		}

		[HttpPost]
		[Authorize]
		[ProducesResponseType(200)]
		[ProducesResponseType(typeof(string), 400)]
		[ProducesResponseType(401)]
		public async Task<IActionResult> AddToCart(AddCartItemModel requestModel)
		{

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
			catch (ArgumentException)
			{
				return BadRequest("Invalid input.");
			}
		}

		[HttpDelete]
		[Authorize]
		[ProducesResponseType(200)]
		[ProducesResponseType(401)]
		public async Task<IActionResult> ClearCart()
		{
			UserModel user = (UserModel)HttpContext.Items["User"]!;

			await _cartService.ClearUserCart(user.Id);
			return Ok();
		}

		[HttpDelete("{id}")]
		[Authorize]
		[ProducesResponseType(200)]
		[ProducesResponseType(typeof(string), 400)]
		[ProducesResponseType(401)]
		public async Task<IActionResult> RemoveFromCart(long id)
		{

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
			catch (ArgumentException)
			{
				return BadRequest("Invalid id was provided.");
			}
		}
	}
}
