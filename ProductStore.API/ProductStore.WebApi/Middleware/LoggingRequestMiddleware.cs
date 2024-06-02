using ProductStoreApi.Extensions;

namespace ProductStoreApi.Middleware
{
	/// <summary>
	/// Middleware for logging HTTP request details.
	/// </summary>
	public class LoggingRequestMiddleware
	{
		private readonly ILogger _logger;
		private readonly RequestDelegate _next;

		public LoggingRequestMiddleware(RequestDelegate next, ILogger<LoggingRequestMiddleware> logger)
		{
			_logger = logger;
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			string path = context.Request.Path;
			string method = context.Request.Method;

			_logger.LogRequest(path, method);

			await _next(context);
		}
	}
}
