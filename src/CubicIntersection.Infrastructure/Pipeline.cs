using CubicIntersection.Application;
using CubicIntersection.Domain;

namespace CubicIntersection.Infrastructure;

public class Pipeline : IPipeline
{
    private readonly IIntersectService _intersectService;
    private readonly IVolumeCalculator _volumeCalculator;
    private readonly IResponseCache _cache;

    public Pipeline(
        IIntersectService intersectService,
        IVolumeCalculator volumeCalculator,
        IResponseCache cache)
    {
        _intersectService = intersectService;
        _volumeCalculator = volumeCalculator;
        _cache = cache;
    }

    public CubicResponse Run(CubicRequest cubicRequest)
    {
        var key = cubicRequest.GetHashCode();

        // Try to retrieve the data from cache
        if (_cache.TryGetValue(key, out CubicResponse cachedResponse))
        {
            return cachedResponse;
        }

        var cubicResponse = _intersectService.Intersects(cubicRequest.First, cubicRequest.Second)
            ? CubicResponse.Success(_volumeCalculator.Intersected(cubicRequest.First, cubicRequest.Second))
            : CubicResponse.Failure();

        _cache.Set(key, cubicResponse);

        return cubicResponse;
    }
}