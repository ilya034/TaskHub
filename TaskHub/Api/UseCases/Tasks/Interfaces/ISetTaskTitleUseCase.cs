namespace Api.UseCases.Tasks.Interfaces;

public interface ISetTaskTitleUseCase
{
    Task SetTaskTitleAsync(Guid taskId, string? title, CancellationToken cancellationToken);
}
