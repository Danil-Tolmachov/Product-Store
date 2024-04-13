using Microsoft.AspNetCore.Mvc;
using StoreBLL.Interfaces.Services;
using StoreBLL.Models;

namespace ProductStoreApi.Controllers
{
	[ApiController]
	[Route("api/")]
	public class HomeController : ControllerBase
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}
	}
}