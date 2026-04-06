namespace Api.Controllers.Tasks.Request;

/// <summary>
/// Request to create a task
/// </summary>
public record CreateTaskRequest
{
    /// <summary>
    /// Task title
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// User identifier who created the task
    /// </summary>
    public Guid CreatedByUserId { get; init; }
}
