using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attributes;

public sealed class ValidateUserRequestAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ActionArguments.TryGetValue("request", out var request) || request is null)
        {
            context.Result = new BadRequestObjectResult("Тело запроса отсутствует");
            return;
        }

        var nameProperty = request.GetType().GetProperty("Name", BindingFlags.Instance | BindingFlags.Public);
        var nameValue = nameProperty?.GetValue(request) as string;

        if (string.IsNullOrWhiteSpace(nameValue))
        {
            context.Result = new BadRequestObjectResult("Имя пользователя не задано");
            return;
        }

        await next();
    }
}
