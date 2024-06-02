using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductStore.Business.Interfaces.Services;
using ProductStore.Business.Models.Dto;

namespace ProductStore.WebApi.Controllers
{
	[ApiVersion(1)]
	[ApiController]
	[Route("/api/v{v:apiVersion}/category")]
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
