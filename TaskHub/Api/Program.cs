using Api.DependencyInjection;
using LoggingLibrary;

namespace Api;

public sealed class Program
{
    public static void Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args)
            .UseInfraSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .Build();

        if (args.Contains("--di-demo", StringComparer.OrdinalIgnoreCase))
        {
            RunDiLifecycleDemo(host.Services);
            return;
        }

        host.Run();
    }

    private static void RunDiLifecycleDemo(IServiceProvider rootServiceProvider)
    {
        Console.WriteLine("DI demo");

        for (int scopeNumber = 1; scopeNumber <= 2; scopeNumber++)
        {
            using IServiceScope scope = rootServiceProvider.CreateScope();
            IServiceProvider scopedProvider = scope.ServiceProvider;

            Console.WriteLine();
            Console.WriteLine($"Scope {scopeNumber}");

            scopedProvider.ResolveTwiceAndPrint<ISingletonServiceOne>();
            scopedProvider.ResolveTwiceAndPrint<ISingletonServiceTwo>();
            scopedProvider.ResolveTwiceAndPrint<IScopedServiceOne>();
            scopedProvider.ResolveTwiceAndPrint<IScopedServiceTwo>();
            scopedProvider.ResolveTwiceAndPrint<ITransientServiceOne>();
            scopedProvider.ResolveTwiceAndPrint<ITransientServiceTwo>();
        }
    }
}
