using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductStoreApi.Extensions;
using StoreBLL.Interfaces.Services;
using StoreBLL.Models.Dto;

namespace ProductStoreApi.Controllers
{
	[ApiController]
	[Route("/api/category")]
	public class CategoryController : ControllerBase
	{
		private readonly ILogger<CategoryController> _logger;
		private readonly ICategoryService _categoryService;
		private readonly IMapper _mapper;

		public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger, IMapper mapper)
		{
			_categoryService = categoryService;
			_logger = logger;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<CategoryBriefDto>), 200)]
		public async Task<ActionResult<IEnumerable<CategoryBriefDto>>> GetCategories()
		{
			_logger.LogRequest(nameof(GetCategories), HttpContext.Request.Method.ToString());

			try
			{
				var models = _mapper.Map<IList<CategoryBriefDto>>(await _categoryService.GetAll());
				return Ok(models);
			}
			catch (Exception ex)
			{
				_logger.LogException(ex, nameof(GetCategories), HttpContext.Request.Method.ToString());
				return new List<CategoryBriefDto>();
			}
		}

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(CategoryDto), 200)]
		[ProducesResponseType(typeof(string), 404)]
		public async Task<ActionResult<CategoryDto>> GetCategory(long id)
		{
			_logger.LogRequest(nameof(GetCategory), HttpContext.Request.Method.ToString());

			try 
			{
				var model = _mapper.Map<CategoryDto>(await _categoryService.GetById(id));
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
