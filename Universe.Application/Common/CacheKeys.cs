using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.Common;

public static class AcademicProgramCacheKeys
{
    private static readonly string Prefix = "academic-programs";
    public static string ById(Guid id) => $"{Prefix}:{id}";
    public static string[] Tags(Guid collegeId) => new[] { $"{Prefix}:{collegeId}" };
    public static string List (
       Guid collegeId,
       string? searchValue,
       string? sortColumn,
       string? sortDirection,
       int pageNumber,
       int pageSize)
    {
        return $"{Prefix}:{collegeId}:list:" +
               $"{searchValue ?? "null"}:" +
               $"{sortColumn ?? "null"}:" +
               $"{sortDirection ?? "null"}:" +
               $"{pageNumber}:{pageSize}";
    }
}


public static class CourseCacheKeys
{
    private static readonly string Prefix = "courses";
    public static string ById(Guid id) => $"{Prefix}:{id}";
    public static string[] Tags(Guid collegeId) => new[] { $"{Prefix}:{collegeId}" };
    public static string List(
       Guid collegeId,
       string? searchValue,
       string? sortColumn,
       string? sortDirection,
       int pageNumber,
       int pageSize)
    {
        return $"{Prefix}:{collegeId}:list:" +
               $"{searchValue ?? "null"}:" +
               $"{sortColumn ?? "null"}:" +
               $"{sortDirection ?? "null"}:" +
               $"{pageNumber}:{pageSize}";
    }
}