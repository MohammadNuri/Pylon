namespace Pylon.API.Middlewares
{
	public static class MiddlewareExtensions
	{
		public static IApplicationBuilder UseRequestLogger(this IApplicationBuilder app)
		{
			return app.Use(async (context, next) =>
			{
				// Log request
				Console.WriteLine($"[{DateTime.Now}] Request: {context.Request.Method} {context.Request.Path}");
				await next();
			});
		}
	}
}
