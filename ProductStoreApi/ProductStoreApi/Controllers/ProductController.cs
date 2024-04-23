using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductStoreApi.Extensions;
using StoreBLL.Interfaces.Services;
using StoreBLL.Models;
using StoreBLL.Models.Dto;
using StoreBLL.Services;

namespace ProductStoreApi.Controllers
{
	[ApiController]
	[Route("/api/product")]
	public class ProductController : ControllerBase
	{
		private readonly ILogger<ProductController> _logger;
		private readonly IProductService _productService;
		private readonly IMapper _mapper;

		public ProductController(IProductService productService, ILogger<ProductController> logger, IMapper mapper)
		{
			_productService = productService;
			_logger = logger;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<ProductDto>), 200)]
		public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
		{
			_logger.LogRequest(nameof(GetProducts), HttpContext.Request.Method.ToString());

			try
			{
				var models = _mapper.Map<IList<ProductDto>>(await _productService.GetAll());
				return Ok(models);
			}
			catch (Exception ex)
			{
				_logger.LogException(ex, nameof(GetProducts), HttpContext.Request.Method.ToString());
				return new List<ProductDto>();
			}
		}

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(ProductDto), 200)]
		[ProducesResponseType(typeof(string), 404)]
		public async Task<ActionResult<ProductDto>> GetProduct(long id)
		{
			_logger.LogRequest(nameof(GetProduct), HttpContext.Request.Method.ToString());

			try
			{
				var model = _mapper.Map<ProductDto>(await _productService.GetById(id));
				return Ok(model);
			}
			catch (ArgumentException ex)
			{
				_logger.LogException(ex, nameof(GetProduct), HttpContext.Request.Method.ToString());
				return NotFound("Product with provided id has not founded.");
			}
		}
	}
}
