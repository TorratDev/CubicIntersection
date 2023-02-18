using CubicIntersection.Application;
using CubicIntersection.Domain;

namespace CubicIntersection.Infrastructure;

public class VolumeCalculator : IVolumeCalculator
{
    public double Intersected(Cubic first, Cubic second)
    {
        var xOverlap = Math.Max(0,
            first.Dimensions.X / 2 + second.Dimensions.X / 2 - Math.Abs(first.Center.X - second.Center.X));
        var yOverlap = Math.Max(0,
            first.Dimensions.Y / 2 + second.Dimensions.Y / 2 - Math.Abs(first.Center.Y - second.Center.Y));
        var zOverlap = Math.Max(0,
            first.Dimensions.Z / 2 + second.Dimensions.Z / 2 - Math.Abs(first.Center.Z - second.Center.Z));
        return xOverlap * yOverlap * zOverlap;
    }
}