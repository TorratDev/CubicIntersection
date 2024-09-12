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

    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        return
            serviceCollection
                .AddSingleton<IPipeline, Pipeline>()
                .AddSingleton<IPipelineTransientAndScoped, PipelineTransientAndScoped>()
                .AddKeyedSingleton<IGenerator, TransientGenerator>("Transient")
                .AddKeyedSingleton<IGenerator, ScopedGenerator>("Scoped")
                .AddKeyedSingleton<IIntersectService, BasicIntersectService>("Basic")
                .AddKeyedSingleton<IIntersectService, MirrorIntersectService>("Mirror")
                .AddSingleton<BasicWrapper>()
                .AddSingleton<MirrorWrapper>()
                .AddSingleton<IVolumeCalculator, VolumeCalculator>()
                .AddSingleton<IResponseCache, ResponseCache>();
    }
}