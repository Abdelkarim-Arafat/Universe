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

public static class StudentCacheKeys
{
    private static readonly string Prefix = "students";
    public static string ById(Guid id) => $"{Prefix}:{id}";
    public static string[] Tags(Guid programId) => new[] { $"{Prefix}:{programId}" };
    public static string List(
       Guid programId,
       string? searchValue,
       int pageNumber,
       int pageSize)
    {
        return $"{Prefix}:{programId}:list:" +
               $"{searchValue ?? "null"}:" +
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


public static class SessionCacheKeys
{
    private const string Prefix = "session";
    public static string CourseSessions(Guid courseOfferingId, int groupNumber)
        => $"{Prefix}:course-offering:{courseOfferingId}:group:{groupNumber}";

    public static string Schedule(Guid programId, Guid semesterId)
        => $"{Prefix}:schedule:program:{programId}:semester:{semesterId}";

    public static string[] Tags(Guid courseOfferingId) => new[] { $"{Prefix}:{courseOfferingId}" };
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

public static class BuildingCacheKeys
{
    private static readonly string Prefix = "buildings";
    public static string RootTag => $"{Prefix}:all";
     
    public static string List(FilterRequest filter) =>
        $"{RootTag}:list:{filter.SearchValue ?? "null"}:{filter.SortColumn ?? "null"}:{filter.SortDirection ?? "null"}:{filter.PageNumber}:{filter.PageSize}";

    public static string ById(Guid id) => $"{Prefix}:{id}";

    public static string[] ListTags() => new[] { RootTag };
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

public static class ControlCacheKeys
{
    private const string Prefix = "control";

    public static string CourseOfferingsStatistics(Guid programId, Guid semesterId)
        => $"{Prefix}:course-offerings-statistics:program:{programId}:semester:{semesterId}";
}

public static class AcademicEventCacheKeys
{
    private const string Prefix = "academic-events";
    public static string List(
        Guid programId,
        Guid semesterId,
        string? sortColumn,
        string? sortDirection,
        int pageNumber,
        int pageSize
    ) =>
        $"{Prefix}:{programId}:{semesterId}:{sortColumn}:{sortDirection}:{pageNumber}:{pageSize}";

    public static string[] Tags(
        Guid programId,
        Guid semesterId
    ) =>
    [
        $"{Prefix}:{programId}",
        $"{Prefix}:{programId}:{semesterId}"
    ];
}

public static class ExamCommitteeCacheKeys
{
    private const string Prefix = "exam-committees";

    public static string List(
        Guid examTermId,
        string? searchValue,
        string? sortColumn,
        string? sortDirection,
        int pageNumber,
        int pageSize
    ) =>
        $"{Prefix}:{examTermId}:{searchValue}:{sortColumn}:{sortDirection}:{pageNumber}:{pageSize}";

    public static string[] Tags(
        Guid examTermId
    ) =>
    [
        $"{Prefix}:{examTermId}"
    ];
}

public static class ExamTermCacheKeys
{
    private const string Prefix = "exam-terms";

    public static string List(
        Guid programId,
        string? searchValue,
        string? sortColumn,
        string? sortDirection,
        int pageNumber,
        int pageSize
    ) =>
        $"{Prefix}:{programId}:{searchValue}:{sortColumn}:{sortDirection}:{pageNumber}:{pageSize}";

    public static string[] Tags(
        Guid programId
    ) =>
    [
        $"{Prefix}:{programId}"
    ];
}