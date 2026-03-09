namespace Api.DependencyInjection;

public abstract class DisposedService : IDisposable, IHasInstanceId
{
    private bool _isDisposed;

    protected DisposedService(string serviceName)
    {
        ServiceName = serviceName;
        InstanceId = Guid.NewGuid();

        Console.WriteLine($"Create  [{ServiceName}] {InstanceId}");
    }

    protected string ServiceName { get; }

    public Guid InstanceId { get; }

    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }

        _isDisposed = true;
        Console.WriteLine($"Dispose [{ServiceName}] {InstanceId}");
        GC.SuppressFinalize(this);
    }
}
