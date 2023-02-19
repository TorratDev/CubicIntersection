using CubicIntersection.Application;
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
        return serviceCollection.AddSingleton<IPipeline, Pipeline>()
            .AddSingleton<IIntersectService, IntersectService>()
            .AddSingleton<IVolumeCalculator, VolumeCalculator>()
            .AddSingleton<IResponseCache, ResponseCache>();
    }
}