using Microsoft.AspNetCore.Mvc;
using ProductStoreApi.Extensions;
using StoreBLL.Interfaces.Services;
using StoreBLL.Models;
using StoreBLL.Services;
using System.Net;

namespace ProductStoreApi.Controllers
{
	[ApiController]
	[Route("/api/image")]
	public class ImageController : ControllerBase
	{
		private readonly ILogger<ImageController> _logger;
		private readonly IProductImageService _imageService;

		public ImageController(IProductImageService imageService, ILogger<ImageController> logger)
		{
			_imageService = imageService;
			_logger = logger;
		}

		[HttpGet("product/{path}")]
		[ProducesResponseType(typeof(byte[]), 200)]
		public async Task<IActionResult> GetProductImage(string path)
		{
			_logger.LogRequest(nameof(GetProductImage), HttpContext.Request.Method.ToString());

			try
			{
				byte[]? image = await _imageService.GetImageByPath(path);

				if (image is null)
                {
					throw new ArgumentException("image");
                }

				return File(image, "image/webp");
			}
			catch (ArgumentException ex)
			{
				_logger.LogException(ex, nameof(GetProductImage), HttpContext.Request.Method.ToString());
				return BadRequest("Image with provided id has not founded.");
			}
		}
	}
}
