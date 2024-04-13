namespace ProductStoreApi.Extensions
{
	public static class LoggerExtension
	{
		public static void LogException(this ILogger logger, Exception ex, string endpointName, string method)
		{
			logger.LogError(ex, "!! {Method}: {EndpointName}", method, endpointName);
		}

		public static void LogRequest(this ILogger logger, string endpointName, string method)
		{
			logger.LogInformation("// {Method}: {EndpointName}", method, endpointName);
		}
	}
}
