using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public sealed class StudentInfoHeadersFilter : IAsyncActionFilter, IOrderedFilter
{
    private readonly string _studentName;
    private readonly string _studentGroup;

    public StudentInfoHeadersFilter(string studentName, string studentGroup)
    {
        _studentName = studentName;
        _studentGroup = studentGroup;
    }

    public int Order => -29;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        context.HttpContext.Response.OnStarting(state =>
        {
            var httpContext = (HttpContext)state!;
            httpContext.Response.Headers["X-Student-Name"] = _studentName;
            httpContext.Response.Headers["X-Student-Group"] = _studentGroup;

            return Task.CompletedTask;
        }, context.HttpContext);

        await next();
    }
}
