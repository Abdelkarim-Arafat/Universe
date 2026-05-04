using Microsoft.Extensions.Caching.Hybrid;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Universe.Core.Interfaces;

namespace Universe.Infrastructure.Repositories;

public class HybridCacheService(HybridCache cache) : ICacheService
{
    private readonly HybridCache _cache = cache;

    public async Task<T> GetOrCreateAsync<T>(
        string key,
        Func<Task<T>> factory,
        CancellationToken cancellationToken,
        string[]? tags = null
        )
    {
        return await _cache.GetOrCreateAsync(
            key,
            async _ => await factory(),
            tags: tags,
            cancellationToken: cancellationToken);
    }
    public async Task RemoveAsync(string key , CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync(key , cancellationToken);
    }
    public async Task RemoveByTagAsync(string[] tags , CancellationToken cancellationToken)
    {
        foreach(var tag in tags)
            await _cache.RemoveByTagAsync(tag , cancellationToken);
    }
}