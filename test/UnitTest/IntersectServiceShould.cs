using CubeIntersection.Domain;
using CubeIntersection.Infrastructure;
using FluentAssertions;
using Xunit;

namespace UnitTest;

public class IntersectServiceShould
{
    [Fact]
    public void Intersect()
    {
        var intersectService = new IntersectService();
        var firstCubic = new Cubic(new Dimensions(2, 2, 2), new Center(1.5, 1.5, 1.5));
        var secondCubic = new Cubic(new Dimensions(2, 2, 2), new Center(0, 0, 0));

        var intersecting = intersectService.Intersects(firstCubic, secondCubic);

        intersecting.Should().BeTrue();
    }

    [Fact]
    public void NotIntersect()
    {
        var intersectService = new IntersectService();
        var firstCubic = new Cubic(new Dimensions(2, 2, 2), new Center(2.5, 2.5, 2.5));
        var secondCubic = new Cubic(new Dimensions(2, 2, 2), new Center(0, 0, 0));

        var intersecting = intersectService.Intersects(firstCubic, secondCubic);

        intersecting.Should().BeFalse();
    }

    [Fact]
    public void ReturnIntersectVolume_0_125()
    {
        var intersectService = new IntersectService();
        var firstCubic = new Cubic(new Dimensions(2, 2, 2), new Center(1.5, 1.5, 1.5));
        var secondCubic = new Cubic(new Dimensions(2, 2, 2), new Center(0, 0, 0));

        var intersectedVolume = intersectService.IntersectedVolume(firstCubic, secondCubic);

        intersectedVolume.Should().Be(0.125);
    }

    [Fact]
    public void ReturnIntersectVolume_0()
    {
        var intersectService = new IntersectService();
        var firstCubic = new Cubic(new Dimensions(2, 2, 2), new Center(2.5, 2.5, 2.5));
        var secondCubic = new Cubic(new Dimensions(2, 2, 2), new Center(0, 0, 0));

        var intersectedVolume = intersectService.IntersectedVolume(firstCubic, secondCubic);

        intersectedVolume.Should().Be(0);
    }
}