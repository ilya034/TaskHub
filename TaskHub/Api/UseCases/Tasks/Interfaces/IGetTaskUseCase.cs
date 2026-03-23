using Api.Controllers.Tasks.Response;

namespace Api.UseCases.Tasks.Interfaces;

public interface IGetTaskUseCase
{
    Task<TaskResponse?> GetTaskByIdAsync(Guid taskId, CancellationToken cancellationToken);
}
