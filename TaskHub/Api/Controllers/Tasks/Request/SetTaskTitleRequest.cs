namespace Api.Controllers.Tasks.Request;

/// <summary>
/// Request to set task title
/// </summary>
public record SetTaskTitleRequest
{
    /// <summary>
    /// Task title
    /// </summary>
    public string? Title { get; init; }
}
