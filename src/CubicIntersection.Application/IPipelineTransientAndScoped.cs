using CubicIntersection.Domain;

namespace CubicIntersection.Application;

public interface IPipelineTransientAndScoped
{
    public AlternativeResponse Run();
}