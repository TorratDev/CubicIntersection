using CubicIntersection.Application;
using CubicIntersection.Domain;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace CubicIntersection.Infrastructure;

public sealed class ResponseCache : IResponseCache
{
    private readonly IMemoryCache _cache;
    private readonly MemoryCacheEntryOptions _cacheEntryOptions;

    public ResponseCache(IMemoryCache cache, IOptionsMonitor<CacheOptions> options)
    {
        _cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(options.CurrentValue.Lifetime);
        _cache = cache;
    }

    public bool TryGetValue<T>(int key, out T item)
    {
        return _cache.TryGetValue(key, out item);
    }

    public void Set<T>(int key, T item)
    {
        _cache.Set(key, item, _cacheEntryOptions);
    }
}