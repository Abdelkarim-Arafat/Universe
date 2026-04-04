using Universe.Application.TeachingSessionServices.SessionDtos;

namespace Universe.Application.CourseServices.Dtos;

public record CourseRegistrationResponse
(
Guid CourseOfferingId,
Guid CourseId,
string CourseName,
string CourseCode,
bool IsOptional,
decimal CreaditHours,
bool IsEnrolled,
List<SessionOptionResponse> Sessions
);
