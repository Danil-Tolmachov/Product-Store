using Microsoft.AspNetCore.Mvc;
using ProductStoreApi.Extensions;
using StoreBLL.Interfaces.Services;
using StoreBLL.Models;
using StoreBLL.Services;

namespace ProductStoreApi.Controllers
{
	[ApiController]
	[Route("/category")]
	public class CategoryController : ControllerBase
	{
		private readonly ILogger<CategoryController> _logger;
		private readonly ICategoryService _categoryService;

		public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
		{
			_categoryService = categoryService;
			_logger = logger;
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<CategoryModel>), 200)]
		public async Task<ActionResult<IEnumerable<CategoryModel>>> GetCategories()
		{
			_logger.LogRequest(nameof(GetCategories), HttpContext.Request.Method.ToString());

			try
			{
				var models = await _categoryService.GetAll();
				return Ok(models);
			}
			catch (Exception ex)
			{
				_logger.LogException(ex, nameof(GetCategories), HttpContext.Request.Method.ToString());
				return new List<CategoryModel>();
			}
		}

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(CategoryModel), 200)]
		[ProducesResponseType(typeof(string), 404)]
		public async Task<ActionResult<CategoryModel>> GetCategory(long id)
		{
			_logger.LogRequest(nameof(GetCategory), HttpContext.Request.Method.ToString());

			try 
			{
				var model = await _categoryService.GetById(id);
				return model;
			}
			catch (ArgumentException ex)
			{
				_logger.LogException(ex, nameof(GetCategory), HttpContext.Request.Method.ToString());
				return NotFound("Category with provided id has not founded.");
			}
		}
	}
}
