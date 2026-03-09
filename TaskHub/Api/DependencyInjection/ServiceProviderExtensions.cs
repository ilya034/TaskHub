namespace Api.DependencyInjection;

public static class ServiceProviderExtensions
{
    public static void ResolveTwiceAndPrint<TService>(this IServiceProvider serviceProvider)
        where TService : class, IHasInstanceId
    {
        TService first = serviceProvider.GetRequiredService<TService>();
        TService second = serviceProvider.GetRequiredService<TService>();

        Console.WriteLine(
            $"{typeof(TService).Name}: first={first.InstanceId}, second={second.InstanceId}, same={ReferenceEquals(first, second)}");
    }
}
