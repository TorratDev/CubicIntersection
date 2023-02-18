using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using CubeIntersection.Domain;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTest;

public class ApiShould : IntegrationTestBase, IClassFixture<WebApplicationFactory<Program>>
{
    private readonly JsonSerializerOptions _options;

    public ApiShould(ITestOutputHelper outputHelper, WebApplicationFactory<Program> factory)
        : base(outputHelper, factory)
    {
        _options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }


    [Theory]
    [InlineData("/api/basic")]
    [InlineData("/api/pipeline")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
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
}