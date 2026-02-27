namespace Api.Middlewares;

public class StudentInfoHeadersMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _studentGroup;
    private readonly string _studentName;

    public StudentInfoHeadersMiddleware(RequestDelegate next, string studentGroup, string studentName)
    {
        _next = next;
        _studentGroup = studentGroup;
        _studentName = studentName;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.OnStarting(() =>
        {
            context.Response.Headers["X-Student-Group"] = _studentGroup;
            context.Response.Headers["X-Student-Name"] = _studentName;
            return Task.CompletedTask;
        });

        await _next(context);
    }
}

