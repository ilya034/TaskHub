using Api.Controllers.Tasks.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public sealed class ValidateSetTaskTitleRequestFilter : IAsyncActionFilter
{
    public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ActionArguments.TryGetValue("request", out var actionArgument) == false ||
            actionArgument is not SetTaskTitleRequest request)
        {
            context.Result = CreateBadRequest("Тело запроса отсутствует");
            return Task.CompletedTask;
        }

        if (string.IsNullOrWhiteSpace(request.Title))
        {
            context.Result = CreateBadRequest("Название задачи не задано");
            return Task.CompletedTask;
        }

        return next();
    }

    private static BadRequestObjectResult CreateBadRequest(string message)
    {
        return new(message);
    }
}
