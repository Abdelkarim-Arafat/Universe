namespace Universe.Application.Common;

public static class AcademicProgramCacheKeys
{
    private static readonly string Prefix = "academic-programs";
    public static string ById(Guid id) => $"{Prefix}:{id}";
    public static string[] Tags(Guid collegeId) => new[] { $"{Prefix}:{collegeId}" };
    public static string List(
       Guid collegeId,
       FilterRequest filter)
    {
        return $"{Prefix}:{collegeId}:list:" +
               $"{filter.SearchValue ?? "null"}:" +
               $"{filter.SortColumn ?? "null"}:" +
               $"{filter.SortDirection ?? "null"}:" +
               $"{filter.PageNumber}:{filter.PageSize}";
    }
}

public static class StudentCacheKeys
{
    private static readonly string Prefix = "students";
    public static string[] Tags() => new[] { $"{Prefix}:all" };
    public static string ById(Guid id) => $"{Prefix}:{id}";
    public static string[] ProgramTag(Guid programId) => new[] { $"{Prefix}:program:{programId}" };
    public static string[] Tags(Guid programId) => new[] { $"{Prefix}:{programId}", $"{Prefix}:all" };
    public static string List(
       Guid programId,
       FilterRequest filter)
    {
        return $"{Prefix}:{programId}:list:" +
               $"{filter.SearchValue ?? "null"}:" +
               $"{filter.PageNumber}:{filter.PageSize}";
    }
}
public static class ServiceCacheKeys
{
    private static readonly string Prefix = "services";
    public static string ById(Guid id) => $"{Prefix}:{id}";
    public static string[] Tags(Guid collegeId) => new[] { $"{Prefix}:{collegeId}" };
    public static string List(
       Guid collegeId,
       FilterRequest filter)
    {
        return $"{Prefix}:{collegeId}:list:" +
               $"{filter.SearchValue ?? "null"}:" +
               $"{filter.SortColumn ?? "null"}:" +
               $"{filter.SortDirection ?? "null"}:" +
               $"{filter.PageNumber}:{filter.PageSize}";
    }
}

public static class ServiceRequestCacheKeys
{
    private static readonly string Prefix = "service-requests";
    public static string ById(Guid id)
        => $"{Prefix}:{id}";
    public static string[] Tags(Guid collegeId)
        => new[] { $"{Prefix}:{collegeId}" };
    public static string PendingList(Guid collegeId, FilterRequest filter)
    {
        return $"{Prefix}:{collegeId}:pending:" +
               $"{filter.SortColumn ?? "null"}:" +
               $"{filter.SortDirection ?? "null"}:" +
               $"{filter.PageNumber}:{filter.PageSize}";
    }
    public static string HistoryList( Guid collegeId, FilterRequest filter)
    {
        return $"{Prefix}:{collegeId}:history:" +
               $"{filter.SearchValue ?? "null"}:" +
               $"{filter.SortColumn ?? "null"}:" +
               $"{filter.SortDirection ?? "null"}:" +
               $"{filter.PageNumber}:{filter.PageSize}";
    }
    public static string StudentHistory(
        Guid studentId,
        FilterRequest filter)
    {
        return $"{Prefix}:student:{studentId}:history:" +
            $"{filter.SortColumn ?? "null"}:" +
               $"{filter.SearchValue ?? "null"}:" +
               $"{filter.SortDirection ?? "null"}:" +
               $"{filter.PageNumber}:{filter.PageSize}";
    }
}

public static class MessageCacheKeys
{
    private static readonly string Prefix = "messages";

    public static string ById(Guid messageId, Guid userId)
        => $"{Prefix}:details:{messageId}:{userId}";

    public static string[] Tags(Guid userId)
        => [$"{Prefix}:{userId}"];

    public static string Inbox(
        Guid userId,
        FilterRequest filter)
        => $"{Prefix}:inbox:" +
        $"{userId}:" +
        $"{filter.PageNumber}:" +
        $"{filter.PageSize}:" +
        $"{filter.SearchValue ?? "null"}";
}
public static class NotificationCacheKeys
{
    private static readonly string Prefix = "notifications";

    public static string[] Tags(Guid userId) => [$"{Prefix}:{userId}"];

    public static string List(
        Guid userId,
        FilterRequest filter)
        => $"{Prefix}:" +
        $"{userId}:" +
        $"{filter.PageNumber}:" +
        $"{filter.PageSize}:";
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
       FilterRequest filter)
    {
        return $"{Prefix}:{collegeId}:list:" +
               $"{filter.SearchValue ?? "null"}:" +
               $"{filter.SortColumn ?? "null"}:" +
               $"{filter.SortDirection ?? "null"}:" +
               $"{filter.PageNumber}:{filter.PageSize}";
    }
}


public static class CourseCacheKeys
{
    private static readonly string Prefix = "courses";
    public static string ById(Guid id) => $"{Prefix}:{id}";
    public static string[] Tags(Guid collegeId) => new[] { $"{Prefix}:{collegeId}" };
    public static string List(
       Guid collegeId,
       FilterRequest filter)
    {
        return $"{Prefix}:{collegeId}:list:" +
               $"{filter.SearchValue ?? "null"}:" +
               $"{filter.SortColumn ?? "null"}:" +
               $"{filter.SortDirection ?? "null"}:" +
               $"{filter.PageNumber}:{filter.PageSize}";
    }
}

public static class StudyLoadByLevelCacheKeys
{
    private static readonly string Prefix = "study-load-by-level";
    public static string ById(Guid id) => $"{Prefix}:{id}";
    public static string[] Tags(Guid programId) => new[] { $"{Prefix}:{programId}" };
    public static string List(
       Guid programId,
       FilterRequest filter)
    {
        return $"{Prefix}:{programId}:list:" +
               $"{filter.SearchValue ?? "null"}:" +
               $"{filter.SortColumn ?? "null"}:" +
               $"{filter.SortDirection ?? "null"}:" +
               $"{filter.PageNumber}:{filter.PageSize}";
    }
}

public static class LevelCacheKeys
{
    private static readonly string Prefix = "levels";
    public static string ById(Guid id) => $"{Prefix}:{id}";
    public static string[] Tags(Guid programId) => new[] { $"{Prefix}:{programId}" };
    public static string List(
       Guid programId,
       FilterRequest filter)
    {
        return $"{Prefix}:{programId}:list:" +
               $"{filter.SearchValue ?? "null"}:" +
               $"{filter.SortColumn ?? "null"}:" +
               $"{filter.SortDirection ?? "null"}:" +
               $"{filter.PageNumber}:{filter.PageSize}";
    }
}

public static class GradeCacheKeys
{
    private static readonly string Prefix = "grades";
    public static string ById(Guid id) => $"{Prefix}:{id}";
    public static string[] Tags(Guid programId) => new[] { $"{Prefix}:{programId}" };
    public static string List(
       Guid programId,
       FilterRequest filter)
    {
        return $"{Prefix}:{programId}:list:" +
               $"{filter.SearchValue ?? "null"}:" +
               $"{filter.SortColumn ?? "null"}:" +
               $"{filter.SortDirection ?? "null"}:" +
               $"{filter.PageNumber}:{filter.PageSize}";
    }
}

public static class RoomCacheKeys
{
    private static readonly string Prefix = "rooms";
    public static string ById(Guid id) => $"{Prefix}:{id}";
    public static string[] Tags(Guid buildingId) => new[] { $"{Prefix}:{buildingId}" };
    public static string List(
       Guid buildingId,
       FilterRequest filter)
    {
        return $"{Prefix}:{buildingId}:list:" +
               $"{filter.SearchValue ?? "null"}:" +
               $"{filter.SortColumn ?? "null"}:" +
               $"{filter.SortDirection ?? "null"}:" +
               $"{filter.PageNumber}:{filter.PageSize}";
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
    public static string ProgramCoursesForExams(
        Guid programId,
        Guid semesterId,
        FilterRequest filter)
    {
        return $"{Prefix}:{programId}:semester:{semesterId}:list:" +
               $"{filter.SearchValue ?? "null"}:" +
               $"{filter.SortColumn ?? "null"}:" +
               $"{filter.SortDirection ?? "null"}:" +
               $"{filter.PageNumber}:{filter.PageSize}";
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
        FilterRequest filter
    ) =>
        $"{Prefix}:{programId}:" +
        $"{semesterId}:" +
        $"{filter.SearchValue ?? "null"}:" +
        $"{filter.SortColumn ?? "null"}:" +
        $"{filter.SortDirection ?? "null"}:" +
        $"{filter.PageNumber}:{filter.PageSize}";

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
        FilterRequest filter
    ) =>
        $"{Prefix}:" +
        $"{examTermId}:" +
        $"{filter.SearchValue ?? "null"}:" +
        $"{filter.SortColumn ?? "null"}:" +
        $"{filter.SortDirection ?? "null"}:" +
        $"{filter.PageNumber}:" +
        $"{filter.PageSize}";

    public static string[] Tags(Guid examTermId) => [$"{Prefix}:{examTermId}"];
}

public static class ExamTermCacheKeys
{
    private const string Prefix = "exam-terms";

    public static string List(
        Guid programId,
        FilterRequest filter
    ) =>
        $"{Prefix}:" +
        $"{programId}:" +
        $"{filter.SearchValue ?? "null"}:" +
        $"{filter.SortColumn ?? "null"}:" +
        $"{filter.SortDirection ?? "null"}:" +
        $"{filter.PageNumber}:" +
        $"{filter.PageSize}";

    public static string[] Tags(Guid programId) =>[$"{Prefix}:{programId}"];
}