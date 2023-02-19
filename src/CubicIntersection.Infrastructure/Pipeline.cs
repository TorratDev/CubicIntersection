using CubicIntersection.Application;
using CubicIntersection.Domain;
using Microsoft.Extensions.Caching.Memory;

namespace CubicIntersection.Infrastructure;

public class Pipeline : IPipeline
{
    private readonly IIntersectService _intersectService;
    private readonly IVolumeCalculator _volumeCalculator;
    private readonly IMemoryCache _cache;
    private readonly MemoryCacheEntryOptions _cacheEntryOptions;

    public Pipeline(
        IIntersectService intersectService,
        IVolumeCalculator volumeCalculator,
        IMemoryCache cache)
    {
        _intersectService = intersectService;
        _volumeCalculator = volumeCalculator;
        _cache = cache;
        _cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromHours(1));
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

        _cache.Set(key, cubicResponse, _cacheEntryOptions);

        return cubicResponse;
    }
}