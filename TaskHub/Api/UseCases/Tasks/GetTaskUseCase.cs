using Api.Controllers.Tasks.Response;
using Api.UseCases.Tasks.Interfaces;
using Logic.Tasks.Services.Interfaces;

namespace Api.UseCases.Tasks;

internal sealed class GetTaskUseCase : IGetTaskUseCase
{
    private readonly ITaskService _taskService;

    public GetTaskUseCase(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public async Task<TaskResponse?> GetTaskByIdAsync(Guid taskId, CancellationToken cancellationToken)
    {
        var task = await _taskService.GetTaskByIdAsync(taskId, cancellationToken);
        if (task is null)
        {
            return null;
        }

        return new TaskResponse(task.Id, task.Title, task.CreatedByUserId, task.CreatedUtc);
    }
}
