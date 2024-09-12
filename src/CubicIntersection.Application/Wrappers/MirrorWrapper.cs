using CubicIntersection.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace CubicIntersection.Application.Wrappers;

public sealed class MirrorWrapper
{
    private readonly IIntersectService _intersectService;

    public MirrorWrapper([FromKeyedServices("Mirror")] IIntersectService intersectService)
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