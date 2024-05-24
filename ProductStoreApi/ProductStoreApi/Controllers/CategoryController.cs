using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StoreBLL.Interfaces.Services;
using StoreBLL.Models.Dto;

namespace ProductStoreApi.Controllers
{
	[ApiController]
	[Route("/api/category")]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _categoryService;
		private readonly IMapper _mapper;

		public CategoryController(ICategoryService categoryService, IMapper mapper)
		{
			_categoryService = categoryService;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<CategoryBriefDto>), 200)]
		public async Task<ActionResult<IEnumerable<CategoryBriefDto>>> GetCategories()
		{

			try
			{
				var models = _mapper.Map<IList<CategoryBriefDto>>(await _categoryService.GetAll());
				return Ok(models);
			}
			catch
			{
				return new List<CategoryBriefDto>();
			}
		}

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(CategoryDto), 200)]
		[ProducesResponseType(typeof(string), 404)]
		public async Task<ActionResult<CategoryDto>> GetCategory(long id)
		{

			try 
			{
				var model = _mapper.Map<CategoryDto>(await _categoryService.GetById(id));
				return model;
			}
			catch
			{
				return NotFound("Category with provided id has not founded.");
			}
		}
	}
}
