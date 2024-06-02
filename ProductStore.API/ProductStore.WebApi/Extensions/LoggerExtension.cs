namespace ProductStoreApi.Extensions
{
	public static class LoggerExtension
	{
		public static void LogInternalError(this ILogger logger, string path, string method)
		{
			logger.LogError("Internal Error on {Method}: {Path}", method, path);
		}

		public static void LogRequest(this ILogger logger, string path, string method)
		{
			logger.LogInformation("Request on {Method}: {Path}", method, path);
		}
	}
}
