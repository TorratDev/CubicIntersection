using CubeIntersection.Domain;

namespace CubeIntersection.Application;

public interface IPipeline
{
    public CubicResponse Run(CubicRequest cubicRequest);
}