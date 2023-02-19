using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTest;

[Collection("IntegrationTest")]
public abstract class IntegrationTestBase
{
    protected readonly HttpClient Client;

    protected IntegrationTestBase(ITestOutputHelper outputHelper,
        WebApplicationFactory<CubicIntersection.Api.Program> factory)
    {
        var server = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddXUnit(outputHelper);
            });

            builder.ConfigureTestServices(collection =>
            {
                collection.AddMvc().AddApplicationPart(typeof(CubicIntersection.Api.Program).Assembly);

                ConfigureServices(collection);
            });

            builder.UseEnvironment("Development");
        });

        Client = server.CreateClient();
    }


    protected virtual void ConfigureServices(IServiceCollection serviceCollection)
    {
    }
}