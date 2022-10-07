using System.Diagnostics;

namespace ExampleApi.Middleware
{
    /// <summary>Adds a response header with the call duration in milliseconds.</summary>
    public class DurationMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var watch = new Stopwatch();
            watch.Start();

            context.Response.OnStarting(() =>
            {
                watch.Stop();
                context.Response.Headers["X-Duration"] = $"{watch.ElapsedMilliseconds}ms";
                return Task.CompletedTask;
            });

            await next(context);
        }
    }

    public static class DurationMiddlewareExtensions
    {
        /// <summary>Adds a response header with the call duration in milliseconds.</summary>
        public static IApplicationBuilder UseDurationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DurationMiddleware>();
        }
    }
}
