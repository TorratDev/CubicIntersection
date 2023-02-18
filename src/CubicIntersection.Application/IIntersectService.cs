using CubicIntersection.Domain;

namespace CubicIntersection.Application;

public interface IIntersectService
{
    public bool Intersects(Cubic first, Cubic second);
    public double IntersectedVolume(Cubic first, Cubic second);
}