using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attributes;

public sealed class ResponseTimeHeaderAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var stopwatch = Stopwatch.StartNew();

        context.HttpContext.Response.OnStarting(state =>
        {
            var httpContext = (HttpContext)state!;
            var elapsedMs = stopwatch.Elapsed.TotalMilliseconds;
            httpContext.Response.Headers["X-Response-Time-Ms"] = elapsedMs.ToString(CultureInfo.InvariantCulture);

            return Task.CompletedTask;
        }, context.HttpContext);

        await next();
    }
}
