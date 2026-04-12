
using Universe.Application.TeachingSessionServices.SessionDtos;

namespace Universe.Application.TeachingSessionServices.Queries.GetCourseSessions;

public record GetCourseSessionsCommand(
    [Required] Guid CourseOfferingId,
    [Required] int GroupNumber
) : IRequest<Result<List<SessionResponse>>>;
