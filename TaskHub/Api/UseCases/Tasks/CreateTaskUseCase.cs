using Api.Controllers.Tasks.Response;
using Api.UseCases.Tasks.Interfaces;
using Logic.Tasks.Services.Interfaces;

namespace Api.UseCases.Tasks;

internal sealed class CreateTaskUseCase : ICreateTaskUseCase
{
    private readonly ITaskService _taskService;

    public CreateTaskUseCase(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public async Task<TaskResponse> CreateTaskAsync(string? title, Guid createdByUserId, CancellationToken cancellationToken)
    {
        var task = await _taskService.CreateTaskAsync(title, createdByUserId, cancellationToken);
        return new TaskResponse(task.Id, task.Title, task.CreatedByUserId, task.CreatedUtc);
    }
}
