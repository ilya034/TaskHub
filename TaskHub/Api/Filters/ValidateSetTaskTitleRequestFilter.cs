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
            context.Result = new BadRequestObjectResult("Тело запроса отсутствует");
            return Task.CompletedTask;
        }

        return next();
    }
}
