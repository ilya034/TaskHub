using Dal.Entities;

namespace Dal.Repositories.Interfaces;

/// <summary>
/// Task repository
/// </summary>
public interface ITaskRepository
{
    Task<TaskEntity> CreateTaskAsync(
        string? title,
        Guid createdByUserId,
        DateTimeOffset createdUtc,
        CancellationToken cancellationToken);

    Task<IReadOnlyCollection<TaskEntity>> GetAllTasksAsync(CancellationToken cancellationToken);

    Task<TaskEntity?> GetTaskByIdAsync(Guid taskId, CancellationToken cancellationToken);

    Task SetTaskTitleAsync(Guid taskId, string? title, CancellationToken cancellationToken);

    Task<bool> DeleteTaskByIdAsync(Guid taskId, CancellationToken cancellationToken);

    Task DeleteAllTasksAsync(CancellationToken cancellationToken);
}
