using Api.Controllers.Tasks.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public class ValidateCreateTaskRequestFilter : IAsyncActionFilter, IOrderedFilter
{
    public int Order => -28;

    public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ActionArguments.TryGetValue("request", out var actionArgument) == false ||
            actionArgument is not CreateTaskRequest request)
        {
            context.Result = new BadRequestObjectResult("Тело запроса отсутствует");
            return Task.CompletedTask;
        }

        if (request.CreatedByUserId == Guid.Empty)
        {
            context.Result = new BadRequestObjectResult("Идентификатор пользователя не задан");
            return Task.CompletedTask;
        }

        if (string.IsNullOrWhiteSpace(request.Title))
        {
            context.Result = new BadRequestObjectResult("Название задачи не задано");
            return Task.CompletedTask;
        }

        return next();
    }
}
