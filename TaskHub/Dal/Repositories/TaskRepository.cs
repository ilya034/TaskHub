using Dal.Context;
using Dal.Entities;
using Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories;

public sealed class TaskRepository : ITaskRepository
{
    private readonly TaskDbContext _dbContext;

    public TaskRepository(TaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TaskEntity> CreateTaskAsync(
        string? title,
        Guid createdByUserId,
        DateTimeOffset createdUtc,
        CancellationToken cancellationToken)
    {
        var taskId = Guid.NewGuid();
        var task = new TaskEntity
        {
            Id = taskId,
            Title = title,
            CreatedByUserId = createdByUserId,
            CreatedUtc = createdUtc
        };

        _dbContext.Tasks.Add(task);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return task;
    }

    public async Task<IReadOnlyCollection<TaskEntity>> GetAllTasksAsync(CancellationToken cancellationToken)
    {
        var tasks = await _dbContext.Tasks
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return tasks.AsReadOnly();
    }

    public async Task<TaskEntity?> GetTaskByIdAsync(Guid taskId, CancellationToken cancellationToken)
    {
        return await _dbContext.Tasks
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == taskId, cancellationToken);
    }

    public async Task SetTaskTitleAsync(Guid taskId, string? title, CancellationToken cancellationToken)
    {
        var task = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == taskId, cancellationToken);
        if (task is null)
        {
            return;
        }

        task.Title = title;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteTaskByIdAsync(Guid taskId, CancellationToken cancellationToken)
    {
        var task = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == taskId, cancellationToken);
        if (task is null)
        {
            return false;
        }

        _dbContext.Tasks.Remove(task);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task DeleteAllTasksAsync(CancellationToken cancellationToken)
    {
        var tasks = await _dbContext.Tasks.ToListAsync(cancellationToken);
        if (tasks.Count is 0)
        {
            return;
        }

        _dbContext.Tasks.RemoveRange(tasks);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
