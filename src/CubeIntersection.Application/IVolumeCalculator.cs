using CubeIntersection.Domain;

namespace CubeIntersection.Application;

public interface IVolumeCalculator
{
    public double Intersected(Cubic first, Cubic second);
}