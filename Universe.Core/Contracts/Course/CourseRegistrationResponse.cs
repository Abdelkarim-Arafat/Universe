
using Universe.Core.Contracts.TeachingSession;

namespace Universe.Core.Contracts.Course;

public record CourseRegistrationResponse (
    Guid CourseOfferingId,
    Guid CourseId,
    string CourseName,
    string CourseCode,
    bool IsOptional,
    decimal CreaditHours,
    bool IsEnrolled,
    List<SessionInfoResponse> Sessions
);
