using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using CubicIntersection.Application;
using CubicIntersection.Domain;
using CubicIntersection.Infrastructure;
using FluentAssertions;
using Moq;
using Xunit;

namespace IntegrationTest;

public class PipelineShould
{
    private readonly Mock<IResponseCache> _mockCache;

    public PipelineShould()
    {
        _mockCache = new Mock<IResponseCache>();
    }

    [Fact]
    public void ReachCalculatorIfIntersectReturnsTrue()
    {
        IFixture fixture = new Fixture();

        var mockIntersectService = new Mock<IIntersectService>();
        mockIntersectService.Setup(service =>
            service.Intersects(It.IsAny<Cubic>(), It.IsAny<Cubic>())
        ).Returns(true);

        var mockVolumeCalculator = new Mock<IVolumeCalculator>();
        mockVolumeCalculator.Setup(calculator =>
            calculator.Intersected(It.IsAny<Cubic>(), It.IsAny<Cubic>())
        ).Returns(fixture.Create<double>());

        var pipeline = new Pipeline(mockIntersectService.Object, mockVolumeCalculator.Object, _mockCache.Object);

        var cubicResponse = pipeline.Run(fixture.Create<CubicRequest>());

        mockVolumeCalculator.Verify(calculator => calculator.Intersected(It.IsAny<Cubic>(), It.IsAny<Cubic>()));

        cubicResponse.IntersectedVolume.Should().BeGreaterThan(0);
        cubicResponse.AreTheyColliding.Should().BeTrue();
    }

    [Theory]
    [InlineData(2, 1.5, 2, 0)]
    [InlineData(2, 0.5, 2, 0)]
    [InlineData(4, 2.5, 2, 0)]
    public void ReturnsSuccessCubicResponse(
        double firstDimension, double firstCenter,
        double secondDimension, double secondCenter)
    {
        var pipeline = BuildPipeline(firstDimension, firstCenter, secondDimension, secondCenter, out var request);

        var cubicResponse = pipeline.Run(request);

        cubicResponse.AreTheyColliding.Should().BeTrue();
        cubicResponse.IntersectedVolume.Should().BeGreaterThan(0);
    }

    [Theory]
    [InlineData(1, 2.5, 2, 0)]
    [InlineData(2, 5.5, 2, 0)]
    [InlineData(2, 2.5, 0.0, 0)]
    [InlineData(2, 2.5, 2, 5)]
    public void ReturnsFailureCubicResponse(
        double firstDimension, double firstCenter,
        double secondDimension, double secondCenter)
    {
        var pipeline = BuildPipeline(firstDimension, firstCenter, secondDimension, secondCenter, out var request);

        var cubicResponse = pipeline.Run(request);

        cubicResponse.AreTheyColliding.Should().BeFalse();
    }

    private Pipeline BuildPipeline(double firstDimension, double firstCenter, double secondDimension,
        double secondCenter, out CubicRequest cubicRequest)
    {
        cubicRequest = BuildCubicRequest(firstDimension, firstCenter, secondDimension, secondCenter);

        return new Pipeline(new IntersectService(), new VolumeCalculator(), _mockCache.Object);
    }

    private static CubicRequest BuildCubicRequest(double firstDimension, double firstCenter, double secondDimension,
        double secondCenter)
    {
        var first = new Cubic(new Dimensions(firstDimension, firstDimension, firstDimension),
            new Center(firstCenter, firstCenter, firstCenter));
        var second = new Cubic(new Dimensions(secondDimension, secondDimension, secondDimension),
            new Center(secondCenter, secondCenter, secondCenter));
        var cubicRequest = new CubicRequest(first, second);
        return cubicRequest;
    }
}