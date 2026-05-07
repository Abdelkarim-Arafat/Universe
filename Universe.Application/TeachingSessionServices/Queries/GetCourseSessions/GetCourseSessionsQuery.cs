
using Universe.Core.Contracts.TeachingSession;

namespace Universe.Application.TeachingSessionServices.Queries.GetCourseSessions;

public record GetCourseSessionsQuery(
    [Required] Guid CourseOfferingId,
    [Required] int GroupNumber
) : IRequest<Result<IReadOnlyList<SessionResponse>>>;
