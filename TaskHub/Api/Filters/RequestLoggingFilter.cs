using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Api.Filters;

public sealed class RequestLoggingFilter : IAsyncActionFilter, IOrderedFilter
{
    private readonly ILogger<RequestLoggingFilter> _logger;

    public RequestLoggingFilter(ILogger<RequestLoggingFilter> logger)
    {
        _logger = logger;
    }

    public int Order => -30;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var request = context.HttpContext.Request;

        _logger.LogInformation(
            "Action started: {HttpMethod} {Path}",
            request.Method,
            request.Path);

        var stopwatch = Stopwatch.StartNew();
        var executedContext = await next();
        stopwatch.Stop();

        var statusCode = executedContext.Exception is not null && executedContext.ExceptionHandled == false
            ? StatusCodes.Status500InternalServerError
            : GetStatusCode(executedContext, context.HttpContext.Response.StatusCode);

        _logger.LogInformation(
            "Action finished: {StatusCode} {ElapsedMilliseconds} ms",
            statusCode,
            stopwatch.ElapsedMilliseconds);
    }

    private static int GetStatusCode(ActionExecutedContext executedContext, int fallbackStatusCode)
    {
        if (executedContext.Result is IStatusCodeActionResult statusCodeActionResult &&
            statusCodeActionResult.StatusCode.HasValue)
        {
            return statusCodeActionResult.StatusCode.Value;
        }

        return fallbackStatusCode;
    }
}
