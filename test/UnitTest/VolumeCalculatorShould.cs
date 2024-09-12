using CubicIntersection.Domain;
using CubicIntersection.Infrastructure;
using FluentAssertions;
using Xunit;

namespace UnitTest;

public class VolumeCalculatorShould
{
    [Fact]
    public void ReturnIntersected_0_125()
    {
        var intersectService = new VolumeCalculator();
        var firstCubic = new Cubic(new Dimension(2, 2, 2), new Center(1.5, 1.5, 1.5));
        var secondCubic = new Cubic(new Dimension(2, 2, 2), new Center(0, 0, 0));

        var intersectedVolume = intersectService.Intersected(firstCubic, secondCubic);

        intersectedVolume.Should().Be(0.125);
    }

    [Fact]
    public void ReturnIntersected_0()
    {
        var intersectService = new VolumeCalculator();
        var firstCubic = new Cubic(new Dimension(2, 2, 2), new Center(2.5, 2.5, 2.5));
        var secondCubic = new Cubic(new Dimension(2, 2, 2), new Center(0, 0, 0));

        var intersectedVolume = intersectService.Intersected(firstCubic, secondCubic);

        intersectedVolume.Should().Be(0);
    }
}