namespace Api.UseCases.Tasks.Interfaces;

public interface IDeleteTasksUseCase
{
    Task DeleteAllTasksAsync(CancellationToken cancellationToken);
}
