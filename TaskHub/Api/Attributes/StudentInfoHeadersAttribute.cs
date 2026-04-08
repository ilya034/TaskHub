using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attributes;

public sealed class StudentInfoHeadersAttribute : Attribute, IAsyncActionFilter
{
    private readonly string _studentName;
    private readonly string _studentGroup;

    public StudentInfoHeadersAttribute(string studentName, string studentGroup)
    {
        _studentName = studentName;
        _studentGroup = studentGroup;
    }

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
