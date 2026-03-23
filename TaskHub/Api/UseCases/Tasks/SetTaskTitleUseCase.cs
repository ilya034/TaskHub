using Api.UseCases.Tasks.Interfaces;
using Logic.Tasks.Services.Interfaces;

namespace Api.UseCases.Tasks;

internal sealed class SetTaskTitleUseCase : ISetTaskTitleUseCase
{
    private readonly ITaskService _taskService;

    public SetTaskTitleUseCase(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public async Task SetTaskTitleAsync(Guid taskId, string? title, CancellationToken cancellationToken)
    {
        await _taskService.SetTaskTitleAsync(taskId, title, cancellationToken);
    }
}
