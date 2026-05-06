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

public static class AcademicYearCacheKeys
{
    private static readonly string Prefix = "academic-years";
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

public static class StudyLoadByLevelCacheKeys
{
    private static readonly string Prefix = "study-load-by-level";
    public static string ById(Guid id) => $"{Prefix}:{id}";
    public static string[] Tags(Guid programId) => new[] { $"{Prefix}:{programId}" };
    public static string List(
       Guid programId,
       string? searchValue,
       string? sortColumn,
       string? sortDirection,
       int pageNumber,
       int pageSize)
    {
        return $"{Prefix}:{programId}:list:" +
               $"{searchValue ?? "null"}:" +
               $"{sortColumn ?? "null"}:" +
               $"{sortDirection ?? "null"}:" +
               $"{pageNumber}:{pageSize}";
    }
}

public static class LevelCacheKeys
{
    private static readonly string Prefix = "levels";
    public static string ById(Guid id) => $"{Prefix}:{id}";
    public static string[] Tags(Guid programId) => new[] { $"{Prefix}:{programId}" };
    public static string List(
       Guid programId,
       string? searchValue,
       string? sortColumn,
       string? sortDirection,
       int pageNumber,
       int pageSize)
    {
        return $"{Prefix}:{programId}:list:" +
               $"{searchValue ?? "null"}:" +
               $"{sortColumn ?? "null"}:" +
               $"{sortDirection ?? "null"}:" +
               $"{pageNumber}:{pageSize}";
    }
}

public static class GradeCacheKeys
{
    private static readonly string Prefix = "grades";
    public static string ById(Guid id) => $"{Prefix}:{id}";
    public static string[] Tags(Guid programId) => new[] { $"{Prefix}:{programId}" };
    public static string List(
       Guid programId,
       string? searchValue,
       string? sortColumn,
       string? sortDirection,
       int pageNumber,
       int pageSize)
    {
        return $"{Prefix}:{programId}:list:" +
               $"{searchValue ?? "null"}:" +
               $"{sortColumn ?? "null"}:" +
               $"{sortDirection ?? "null"}:" +
               $"{pageNumber}:{pageSize}";
    }
}

public static class RoomCacheKeys
{
    private static readonly string Prefix = "rooms";
    public static string ById(Guid id) => $"{Prefix}:{id}";
    public static string[] Tags(Guid buildingId) => new[] { $"{Prefix}:{buildingId}" };
    public static string List(
       Guid buildingId,
       string? searchValue,
       string? sortColumn,
       string? sortDirection,
       int pageNumber,
       int pageSize)
    {
        return $"{Prefix}:{buildingId}:list:" +
               $"{searchValue ?? "null"}:" +
               $"{sortColumn ?? "null"}:" +
               $"{sortDirection ?? "null"}:" +
               $"{pageNumber}:{pageSize}";
    }

    public static string AvailableForCommittees(Guid buildingId, Guid examTermId, FilterRequest filter)
    {
        return $"{Prefix}:{buildingId}:term:{examTermId}:available:" +
               $"{filter.SearchValue ?? "null"}:" +
               $"{filter.SortColumn ?? "null"}:" +
               $"{filter.SortDirection ?? "null"}:" +
               $"{filter.PageNumber}:{filter.PageSize}";
    }

 
    public static string[] CommitteeTags(Guid buildingId, Guid examTermId) =>
        new[] { $"{Prefix}:{buildingId}", $"{Prefix}:exam-term:{examTermId}" };

}