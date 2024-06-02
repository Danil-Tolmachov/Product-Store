using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ProductStore.Business.Interfaces.Services;

namespace ProductStore.WebApi.Controllers
{
	[ApiVersion(1)]
	[ApiController]
	[Route("/api/v{v:apiVersion}/image")]
	public class ImageController : ControllerBase
	{
		private readonly IProductImageService _imageService;

		public ImageController(IProductImageService imageService)
		{
			_imageService = imageService;
		}

		[HttpGet("product/{path}")]
		[ProducesResponseType(typeof(byte[]), 200)]
		public async Task<IActionResult> GetProductImage(string path)
		{

			try
			{
				byte[]? image = await _imageService.GetImageByPath(path);

				if (image is null)
                {
					throw new ArgumentException("image");
                }

				return File(image, "image/webp");
			}
			catch
			{
				return BadRequest("Image with provided id has not founded.");
			}
		}
	}
}
