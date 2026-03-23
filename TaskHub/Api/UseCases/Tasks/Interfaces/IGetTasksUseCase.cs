using Api.Controllers.Tasks.Response;

namespace Api.UseCases.Tasks.Interfaces;

public interface IGetTasksUseCase
{
    Task<TaskListResponse> GetAllTasksAsync(CancellationToken cancellationToken);
}
