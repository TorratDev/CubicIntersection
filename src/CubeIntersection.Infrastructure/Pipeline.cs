using CubeIntersection.Application;
using CubeIntersection.Domain;

namespace CubeIntersection.Infrastructure;

public class Pipeline : IPipeline
{
    private readonly IIntersectService _intersectService;
    private readonly IVolumeCalculator _volumeCalculator;

    public Pipeline(
        IIntersectService intersectService,
        IVolumeCalculator volumeCalculator)
    {
        _intersectService = intersectService;
        _volumeCalculator = volumeCalculator;
    }

    public CubicResponse Run(CubicRequest cubicRequest)
    {
        return _intersectService.Intersects(cubicRequest.First, cubicRequest.Second)
            ? CubicResponse.Success(_volumeCalculator.Intersected(cubicRequest.First, cubicRequest.Second))
            : CubicResponse.Failure();
    }
}