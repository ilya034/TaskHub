namespace Api.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddDiLifecycleDemoServices(this IServiceCollection services)
    {
        services.AddSingleton<ISingletonServiceOne, SingletonServiceOne>();
        services.AddSingleton<ISingletonServiceTwo, SingletonServiceTwo>();

        services.AddScoped<IScopedServiceOne, ScopedServiceOne>();
        services.AddScoped<IScopedServiceTwo, ScopedServiceTwo>();

        services.AddTransient<ITransientServiceOne, TransientServiceOne>();
        services.AddTransient<ITransientServiceTwo, TransientServiceTwo>();
    }
}
