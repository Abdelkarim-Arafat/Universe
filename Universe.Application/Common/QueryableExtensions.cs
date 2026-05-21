using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq.Dynamic.Core;

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

    public static IQueryable<T> ApplySort<T>(
    this IQueryable<T> query,
    string? sortColumn)
    {
        if (!string.IsNullOrWhiteSpace(sortColumn))
        {
            query = query.OrderBy(sortColumn);
        }

        return query;
    }
}