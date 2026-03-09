namespace Api.DependencyInjection;

public interface ISingletonServiceOne : IHasInstanceId;
public interface ISingletonServiceTwo : IHasInstanceId;

public interface IScopedServiceOne : IHasInstanceId;
public interface IScopedServiceTwo : IHasInstanceId;

public interface ITransientServiceOne : IHasInstanceId;
public interface ITransientServiceTwo : IHasInstanceId;

public sealed class SingletonServiceOne : DisposedService, ISingletonServiceOne
{
    public SingletonServiceOne() : base(nameof(ISingletonServiceOne))
    {
    }
}

public sealed class SingletonServiceTwo : DisposedService, ISingletonServiceTwo
{
    public SingletonServiceTwo() : base(nameof(ISingletonServiceTwo))
    {
    }
}

public sealed class ScopedServiceOne : DisposedService, IScopedServiceOne
{
    public ScopedServiceOne() : base(nameof(IScopedServiceOne))
    {
    }
}

public sealed class ScopedServiceTwo : DisposedService, IScopedServiceTwo
{
    public ScopedServiceTwo() : base(nameof(IScopedServiceTwo))
    {
    }
}

public sealed class TransientServiceOne : DisposedService, ITransientServiceOne
{
    public TransientServiceOne() : base(nameof(ITransientServiceOne))
    {
    }
}

public sealed class TransientServiceTwo : DisposedService, ITransientServiceTwo
{
    public TransientServiceTwo() : base(nameof(ITransientServiceTwo))
    {
    }
}
