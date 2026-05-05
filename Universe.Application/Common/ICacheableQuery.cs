using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.Common;

public interface ICacheableQuery
{
    string CacheKey { get; }
    string[] Tags { get; }
    TimeSpan? Expiration { get; }
}