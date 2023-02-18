using CubicIntersection.Domain;

namespace CubicIntersection.Application;

public interface IPipeline
{
    public CubicResponse Run(CubicRequest cubicRequest);
}