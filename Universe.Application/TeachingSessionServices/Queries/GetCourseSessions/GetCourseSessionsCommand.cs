
using Universe.Application.TeachingSessionServices.SessionDtos;

namespace Universe.Application.TeachingSessionServices.Queries.GetCourseSessions;

public record GetCourseSessionsCommand(
    [Required] Guid CourseOfferingId,
    int GroupNumber
) : IRequest<Result<List<SessionResponse>>>;
