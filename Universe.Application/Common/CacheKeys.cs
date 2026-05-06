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
public static class ServiceCacheKeys
{
    private static readonly string Prefix = "services";
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

public static class ServiceRequestCacheKeys
{
    private static readonly string Prefix = "service-requests";
    public static string ById(Guid id)
        => $"{Prefix}:{id}";
    public static string[] Tags(Guid collegeId)
        => new[] { $"{Prefix}:{collegeId}" };
    public static string PendingList(
        Guid collegeId,
        string? sortColumn,
        string? sortDirection,
        int pageNumber,
        int pageSize)
    {
        return $"{Prefix}:{collegeId}:pending:" +
               $"{sortColumn ?? "null"}:" +
               $"{sortDirection ?? "null"}:" +
               $"{pageNumber}:{pageSize}";
    }
    public static string HistoryList(
        Guid collegeId,
        string? sortColumn,
        string? sortDirection,
        int pageNumber,
        int pageSize)
    {
        return $"{Prefix}:{collegeId}:history:" +
               $"{sortColumn ?? "null"}:" +
               $"{sortDirection ?? "null"}:" +
               $"{pageNumber}:{pageSize}";
    }
    public static string StudentHistory(
        Guid studentId,
        string? sortColumn,
        string? sortDirection,
        int pageNumber,
        int pageSize)
    {
        return $"{Prefix}:student:{studentId}:history:" +
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

public static class CourseOfferingCacheKeys
{
    private static readonly string Prefix = "course-offerings";
    public static string ById(Guid id)
        => $"{Prefix}:{id}";
    public static string[] Tags(Guid programId)
        => new[] { $"{Prefix}:{programId}" };
    public static string LevelCourses(
        Guid levelId,
        Guid semesterId)
    {
        return $"{Prefix}:level:{levelId}:semester:{semesterId}";
    }
    public static string ProgramCoursesForExams (
        Guid programId,
        Guid semesterId,
        string? searchValue,
        string? sortColumn,
        string? sortDirection,
        int pageNumber,
        int pageSize)
    {
        return $"{Prefix}:{programId}:semester:{semesterId}:list:" +
               $"{searchValue ?? "null"}:" +
               $"{sortColumn ?? "null"}:" +
               $"{sortDirection ?? "null"}:" +
               $"{pageNumber}:{pageSize}";
    }
}