namespace Api.DependencyInjection;

public interface IHasInstanceId
{
    Guid InstanceId { get; }
}
