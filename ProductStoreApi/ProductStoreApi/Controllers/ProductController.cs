using Microsoft.AspNetCore.Mvc;
using ProductStoreApi.Extensions;
using StoreBLL.Interfaces.Services;
using StoreBLL.Models;
using StoreBLL.Services;

namespace ProductStoreApi.Controllers
{
	[ApiController]
	[Route("/api/product")]
	public class ProductController : ControllerBase
	{
		private readonly ILogger<ProductController> _logger;
		private readonly IProductService _productService;

		public ProductController(IProductService productService, ILogger<ProductController> logger)
		{
			_productService = productService;
			_logger = logger;
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<ProductModel>), 200)]
		public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts()
		{
			_logger.LogRequest(nameof(GetProducts), HttpContext.Request.Method.ToString());

			try
			{
				var models = await _productService.GetAll();
				return Ok(models);
			}
			catch (Exception ex)
			{
				_logger.LogException(ex, nameof(GetProducts), HttpContext.Request.Method.ToString());
				return new List<ProductModel>();
			}
		}

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(ProductModel), 200)]
		[ProducesResponseType(typeof(string), 404)]
		public async Task<ActionResult<ProductModel>> GetProduct(long id)
		{
			_logger.LogRequest(nameof(GetProduct), HttpContext.Request.Method.ToString());

			try
			{
				var model = await _productService.GetById(id);
				return model;
			}
			catch (ArgumentException ex)
			{
				_logger.LogException(ex, nameof(GetProduct), HttpContext.Request.Method.ToString());
				return NotFound("Product with provided id has not founded.");
			}
		}
	}
}
