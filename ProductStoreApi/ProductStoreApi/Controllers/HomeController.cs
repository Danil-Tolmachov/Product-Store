using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ProductStoreApi.Controllers
{
	[ApiVersion(1)]
	[ApiController]
	[Route("api/v{v:apiVersion}/")]
	public class HomeController : ControllerBase
	{
	}
}