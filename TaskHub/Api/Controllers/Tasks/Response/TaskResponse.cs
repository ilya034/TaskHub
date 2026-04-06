namespace Api.Controllers.Tasks.Response;

public record TaskResponse
{
    public Guid Id { get; }
    
    public string? Title { get; }
    
    public Guid CreatedByUserId { get; }

    public DateTimeOffset CreatedUtc { get; }

    public TaskResponse(Guid id, string? title, Guid createdByUserId, DateTimeOffset createdUtc)
    {
        Id = id;
        Title = title;
        CreatedByUserId = createdByUserId;
        CreatedUtc = createdUtc;
    }
}
