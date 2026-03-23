namespace Api.UseCases.Tasks.Interfaces;

public interface IDeleteTaskUseCase
{
    Task<bool> DeleteTaskByIdAsync(Guid taskId, CancellationToken cancellationToken);
}
