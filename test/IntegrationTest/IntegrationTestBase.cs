using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTest;

public abstract class IntegrationTestBase
{
    protected HttpClient Client;

    protected IntegrationTestBase(ITestOutputHelper outputHelper, WebApplicationFactory<Program> factory)
    {
        factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddXUnit(outputHelper);
            });

            builder.ConfigureTestServices(collection =>
            {
                collection.AddMvc().AddApplicationPart(typeof(Program).Assembly);

                ConfigureServices(collection);
            });

            builder.UseEnvironment("Development");
        });

        Client = factory.CreateClient();
    }


    protected virtual void ConfigureServices(IServiceCollection serviceCollection)
    {
    }
}