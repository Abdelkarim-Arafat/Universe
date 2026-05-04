using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.Common;

public class CachingBehavior<TRequest, TResponse>(
    ICacheService cacheService
) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is not ICacheableQuery cacheable)
            return await next();

        return await cacheService.GetOrCreateAsync(
            cacheable.CacheKey,
            async () => await next(),
            cancellationToken: cancellationToken,
            cacheable.Tags
        );
    }
}