using System.Diagnostics;

namespace Api.Middlewares;

/// <summary>
/// Измеряет время обработки запроса и добавляет его в заголовок ответа "X-Response-Time-Ms"
/// </summary>
public sealed class ResponseTimeMiddleware
{
    private const string HeaderName = "X-Response-Time-Ms";
    private readonly RequestDelegate _next;

    public ResponseTimeMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        context.Response.OnStarting(() =>
        {
            stopwatch.Stop();
            context.Response.Headers[HeaderName] = stopwatch.ElapsedMilliseconds.ToString();
            return Task.CompletedTask;
        });

        await _next(context);
    }
}
