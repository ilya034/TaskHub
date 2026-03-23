using Api.UseCases.Tasks.Interfaces;
using Logic.Tasks.Services.Interfaces;

namespace Api.UseCases.Tasks;

internal sealed class DeleteTaskUseCase : IDeleteTaskUseCase
{
    private readonly ITaskService _taskService;

    public DeleteTaskUseCase(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public async Task<bool> DeleteTaskByIdAsync(Guid taskId, CancellationToken cancellationToken)
    {
        return await _taskService.DeleteTaskByIdAsync(taskId, cancellationToken);
    }
}
