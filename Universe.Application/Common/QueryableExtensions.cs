using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Universe.Application.Common;

public static class QueryableExtensions
{
    public static IQueryable<T> ApplySearch<T> (
        this IQueryable<T> query,
        string? searchValue,
        Func<T, string> selector)
    {
        if (string.IsNullOrWhiteSpace(searchValue))
            return query;

        return query.Where(x => selector(x).Contains(searchValue));
    }

    public static IQueryable<T> ApplySort<T, TKey>(
    this IQueryable<T> query,
    bool isDescending,
    Expression<Func<T, TKey>> keySelector)
    {
        return isDescending
            ? query.OrderByDescending(keySelector)
            : query.OrderBy(keySelector);
    }
}