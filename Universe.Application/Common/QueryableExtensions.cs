using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Universe.Application.Common;

public static class QueryableExtensions
{
    public static IQueryable<T> ApplySearch<T>(
    this IQueryable<T> query,
    string? searchValue,
    Func<T, string> selector1,
    Func<T, string>? selector2 = null)
    {
        if (string.IsNullOrWhiteSpace(searchValue))
            return query;

        return query.Where(x =>
            (selector1(x)!.Contains(searchValue)) ||
            (selector2 != null && selector2(x)!.Contains(searchValue))
        );
    }

    public static IQueryable<T> ApplySort<T, TKey>(
    this IQueryable<T> query,
    string sortDirection,
    Expression<Func<T, TKey>> keySelector)
    {
        return sortDirection.ToLower() == "desc"
            ? query.OrderByDescending(keySelector)
            : query.OrderBy(keySelector);
    }
}