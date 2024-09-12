using CubicIntersection.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace CubicIntersection.Application.Wrappers;

public sealed class BasicWrapper
{
    private readonly IIntersectService _intersectService;

    public BasicWrapper([FromKeyedServices("Basic")] IIntersectService intersectService)
    {
        _intersectService = intersectService;
    }

    public bool Intersects(Cubic first, Cubic second)
    {
        return _intersectService.Intersects(first, second);
    }

    public double IntersectedVolume(Cubic first, Cubic second)
    {
        return _intersectService.IntersectedVolume(first, second);
    }
}