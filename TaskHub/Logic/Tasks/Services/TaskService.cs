using Dal.Repositories.Interfaces;
using Logic.Tasks.Models;
using Logic.Tasks.Services.Interfaces;

namespace Logic.Tasks.Services;

internal sealed class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskModel> CreateTaskAsync(string? title, Guid createdByUserId, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.CreateTaskAsync(title, createdByUserId, DateTimeOffset.UtcNow, cancellationToken);
        return new TaskModel(task.Id, task.Title, task.CreatedByUserId, task.CreatedUtc);
    }

    public async Task<IReadOnlyCollection<TaskModel>> GetAllTasksAsync(CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetAllTasksAsync(cancellationToken);

        var result = tasks
            .Select(x => new TaskModel(x.Id, x.Title, x.CreatedByUserId, x.CreatedUtc))
            .ToList()
            .AsReadOnly();

        return result;
    }

    public async Task<TaskModel?> GetTaskByIdAsync(Guid taskId, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetTaskByIdAsync(taskId, cancellationToken);
        if (task is null)
        {
            return null;
        }

        return new TaskModel(task.Id, task.Title, task.CreatedByUserId, task.CreatedUtc);
    }

    public async Task SetTaskTitleAsync(Guid taskId, string? title, CancellationToken cancellationToken)
    {
        await _taskRepository.SetTaskTitleAsync(taskId, title, cancellationToken);
    }

    public async Task<bool> DeleteTaskByIdAsync(Guid taskId, CancellationToken cancellationToken)
    {
        return await _taskRepository.DeleteTaskByIdAsync(taskId, cancellationToken);
    }

    public async Task DeleteAllTasksAsync(CancellationToken cancellationToken)
    {
        await _taskRepository.DeleteAllTasksAsync(cancellationToken);
    }
}
