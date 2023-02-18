using CubicIntersection.Domain;

namespace CubicIntersection.Application;

public interface IVolumeCalculator
{
    public double Intersected(Cubic first, Cubic second);
}