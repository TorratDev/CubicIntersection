using CubicIntersection.Application;
using CubicIntersection.Domain;

namespace CubicIntersection.Infrastructure;

public class VolumeCalculator : IVolumeCalculator
{
    public double Intersected(Cubic first, Cubic second)
    {
        var xOverlap = Math.Max(0,
            first.Dimension.X / 2 + second.Dimension.X / 2 - Math.Abs(first.Center.X - second.Center.X));
        var yOverlap = Math.Max(0,
            first.Dimension.Y / 2 + second.Dimension.Y / 2 - Math.Abs(first.Center.Y - second.Center.Y));
        var zOverlap = Math.Max(0,
            first.Dimension.Z / 2 + second.Dimension.Z / 2 - Math.Abs(first.Center.Z - second.Center.Z));
        return xOverlap * yOverlap * zOverlap;
    }
}