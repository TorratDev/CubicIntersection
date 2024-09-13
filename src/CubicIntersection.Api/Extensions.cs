using CubicIntersection.Application;
using CubicIntersection.Application.Wrappers;
using CubicIntersection.Infrastructure;

namespace CubicIntersection.Api;

public static class Extensions
{
    public static IConfigurationBuilder AddAppSettingsConfiguration(this IConfigurationBuilder configurationBuilder,
        IHostEnvironment environment)
    {
        environment.EnvironmentName = environment.IsDevelopment() ? "Development" : "Production";

        return configurationBuilder
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
    }

    public const string Singleton = "Singleton";
    public const string Scoped = "Scoped";
    public const string Transient = "Transient";

    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        return
            serviceCollection
                .AddSingleton<IPipeline, Pipeline>()
                .AddTransient<IPipelineTransientAndScoped, PipelineTransientAndScoped>()
                .AddKeyedTransient<IGenerator, TransientGenerator>(Transient)
                .AddKeyedScoped<IGenerator, ScopedGenerator>(Scoped)
                .AddKeyedSingleton<IGenerator, SingletonGenerator>(Singleton)
                .AddKeyedSingleton<IIntersectService, BasicIntersectService>("Basic")
                .AddKeyedSingleton<IIntersectService, MirrorIntersectService>("Mirror")
                .AddSingleton<BasicWrapper>()
                .AddSingleton<MirrorWrapper>()
                .AddSingleton<IVolumeCalculator, VolumeCalculator>()
                .AddSingleton<IResponseCache, ResponseCache>();
    }
}