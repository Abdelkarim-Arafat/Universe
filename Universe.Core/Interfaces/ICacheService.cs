using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Interfaces;

public interface ICacheService
{
    Task<T> GetOrCreateAsync<T>(
        string key,
        Func<Task<T>> factory,
        CancellationToken cancellationToken,
        string[]? tags = null);
    Task RemoveAsync(string key, CancellationToken cancellationToken);
    Task RemoveByTagAsync(string[] tags, CancellationToken cancellationToken);
}