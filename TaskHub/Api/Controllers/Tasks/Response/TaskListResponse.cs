namespace Api.Controllers.Tasks.Response;

public record TaskListResponse
{
    public IReadOnlyCollection<TaskResponse> TaskList { get; init; }

    public TaskListResponse(IReadOnlyCollection<TaskResponse> taskList)
    {
        TaskList = taskList;
    }
}
