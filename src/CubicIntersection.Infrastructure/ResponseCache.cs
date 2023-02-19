using CubicIntersection.Application;
using Microsoft.Extensions.Caching.Memory;

namespace CubicIntersection.Infrastructure;

public sealed class ResponseCache : IResponseCache
{
    private readonly IMemoryCache _cache;
    private readonly MemoryCacheEntryOptions _cacheEntryOptions;

    public ResponseCache(IMemoryCache cache)
    {
        _cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromHours(1));
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