using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using AutoFixture;
using CubicIntersection.Application;
using CubicIntersection.Domain;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTest;

public class ApiShould : IntegrationTestBase, IClassFixture<WebApplicationFactory<CubicIntersection.Api.Program>>
{
    private readonly JsonSerializerOptions _options;
    private Mock<IResponseCache> _mockCache;

    public ApiShould(ITestOutputHelper outputHelper, WebApplicationFactory<CubicIntersection.Api.Program> factory)
        : base(outputHelper, factory)
    {
        _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }


    [Theory]
    [InlineData("/api/basic")]
    [InlineData("/api/pipeline")]
    public async Task ReturnSuccessResponse(string url)
    {
        var first = new Cubic(new Dimensions(2, 2, 2), new Center(1.5, 1.5, 1.5));
        var second = new Cubic(new Dimensions(2, 2, 2), new Center(0, 0, 0));
        var cubicRequest = new CubicRequest(first, second);

        var jsonContent = JsonContent.Create(cubicRequest);

        var httpResponseMessage = await Client.PostAsync(url, jsonContent);

        var stream = await httpResponseMessage.Content.ReadAsStreamAsync();

        var response = await JsonSerializer.DeserializeAsync<CubicResponse>(stream, _options);

        response.AreTheyColliding.Should().BeTrue();
        response.IntersectedVolume.Should().Be(0.125);
    }

    [Theory]
    [InlineData("/api/pipeline")]
    public async Task ReturnSuccessResponseFromCache(string url)
    {
        var fixture = new Fixture();
        fixture.Customizations.Add(new RandomNumericSequenceGenerator(2, 10));
        var tries = fixture.Create<int>();

        for (var i = 0; i < tries; i++)
        {
            await RunAndAssertPost(url);
        }

        _mockCache.Verify(cache => cache.TryGetValue(It.IsAny<int>(), out It.Ref<CubicResponse>.IsAny),
            Times.Exactly(tries));
    }

    private async Task RunAndAssertPost(string url)
    {
        var first = new Cubic(new Dimensions(2, 2, 2), new Center(1.5, 1.5, 1.5));
        var second = new Cubic(new Dimensions(2, 2, 2), new Center(0, 0, 0));
        var cubicRequest = new CubicRequest(first, second);

        var jsonContent = JsonContent.Create(cubicRequest);

        var httpResponseMessage = await Client.PostAsync(url, jsonContent);

        var stream = await httpResponseMessage.Content.ReadAsStreamAsync();

        var response = await JsonSerializer.DeserializeAsync<CubicResponse>(stream, _options);

        response.AreTheyColliding.Should().BeTrue();
        response.IntersectedVolume.Should().Be(0.125);
    }

    protected override void ConfigureServices(IServiceCollection serviceCollection)
    {
        _mockCache = new Mock<IResponseCache>();

        var serviceDescriptor = new ServiceDescriptor(typeof(IResponseCache), _mockCache.Object);
        serviceCollection.Replace(serviceDescriptor);
        base.ConfigureServices(serviceCollection);
    }
}