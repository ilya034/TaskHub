using Api.UseCases.Tasks.Interfaces;
using Logic.Tasks.Services.Interfaces;

namespace Api.UseCases.Tasks;

internal sealed class DeleteTasksUseCase : IDeleteTasksUseCase
{
    private readonly ITaskService _taskService;

    public DeleteTasksUseCase(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public async Task DeleteAllTasksAsync(CancellationToken cancellationToken)
    {
        await _taskService.DeleteAllTasksAsync(cancellationToken);
    }
}
