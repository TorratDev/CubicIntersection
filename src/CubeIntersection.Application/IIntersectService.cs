using CubeIntersection.Domain;

namespace CubeIntersection.Application;

public interface IIntersectService
{
    public bool Intersects(Cubic first, Cubic second);
    public double IntersectedVolume(Cubic first, Cubic second);
}