using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.ModelBinding;

public sealed class FromRouteTaskIdModelBinder : IModelBinder
{
    private const string RouteParameterName = "id";

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);

        if (bindingContext.ActionContext.RouteData.Values.TryGetValue(RouteParameterName, out var routeValue) == false ||
            string.IsNullOrWhiteSpace(routeValue?.ToString()))
        {
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Идентификатор задачи не задан");
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        var rawTaskId = routeValue!.ToString()!;
        if (Guid.TryParse(rawTaskId, out var taskId) == false)
        {
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Идентификатор задачи имеет некорректный формат");
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        bindingContext.Result = ModelBindingResult.Success(taskId);
        return Task.CompletedTask;
    }
}
