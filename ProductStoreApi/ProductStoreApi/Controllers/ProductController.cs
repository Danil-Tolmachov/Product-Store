using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductStoreApi.Extensions;
using StoreBLL.Interfaces.Services;
using StoreBLL.Models;
using StoreBLL.Models.Dto;
using StoreBLL.Services;
using System.Collections.Generic;

namespace ProductStoreApi.Controllers
{
	[ApiController]
	[Route("/api/product")]
	public class ProductController : ControllerBase
	{
		private readonly ILogger<ProductController> _logger;
		private readonly IProductService _productService;
		private readonly ICategoryService _categoryService;
		private readonly IMapper _mapper;

		private const int productsPerPage = 10;

		public ProductController(IProductService productService, ICategoryService categoryService, ILogger<ProductController> logger, IMapper mapper)
		{
			_productService = productService;
			_categoryService = categoryService;
			_logger = logger;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<ProductPageDto>), 200)]
		public async Task<ActionResult<IEnumerable<ProductPageDto>>> GetProducts([FromQuery] int? page, [FromQuery] int count = productsPerPage)
		{
			_logger.LogRequest(nameof(GetProducts), HttpContext.Request.Method.ToString());

			try
			{
				IList<ProductDto> products;
				ProductPageDto dto;

				// Use pagination if "page" param is used
				if (page is not null)
				{
					products = _mapper.Map<IList<ProductDto>>(await _productService.GetAll((int)page, count));

					dto = new()
					{
						Products = products,
						CurrentPage = page,
						PagesCount = await _productService.CountPages(count),
						PageSize = count,
						TotalCount = await _productService.Count(),
					};
				}
				else
				{
					products = _mapper.Map<IList<ProductDto>>(await _productService.GetAll());

					dto = new()
					{
						Products = products,
						TotalCount = await _productService.Count(),
					};
				}

				return Ok(dto);
			}
			catch (Exception ex)
			{
				_logger.LogException(ex, nameof(GetProducts), HttpContext.Request.Method.ToString());
				return new List<ProductPageDto>();
			}
		}

		[HttpGet("category/{id}")]
		[ProducesResponseType(typeof(IEnumerable<ProductPageDto>), 200)]
		public async Task<ActionResult<IEnumerable<ProductPageDto>>> GetProductsByCategory(long id, [FromQuery] int? page, [FromQuery] int count = productsPerPage)
		{
			_logger.LogRequest(nameof(GetProductsByCategory), HttpContext.Request.Method.ToString());

			try
			{
				IList<ProductDto> products;
				ProductPageDto dto;

				// Use pagination if "page" param is used
				if (page is not null)
				{
					products = _mapper.Map<IList<ProductDto>>(await _categoryService.GetProducts(id, (int)page, count));

					dto = new()
					{
						Products = products,
						CurrentPage = page,
						PagesCount = await _productService.CountPagesByCategory(id, count),
						PageSize = count,
						TotalCount = await _productService.CountByCategory(id),
					};
				}
				else
				{
					products = _mapper.Map<IList<ProductDto>>(await _categoryService.GetProducts(id));

					dto = new()
					{
						Products = products,
						TotalCount = await _productService.CountByCategory(id),
					};
				}

				return Ok(dto);
			}
			catch (Exception ex)
			{
				_logger.LogException(ex, nameof(GetProductsByCategory), HttpContext.Request.Method.ToString());
				return new List<ProductPageDto>();
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
