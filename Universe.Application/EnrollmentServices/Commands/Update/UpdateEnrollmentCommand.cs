using Universe.Application.EnrollmentServices.Dtos;

namespace Universe.Application.EnrollmentServices.Commands.Update;

public record UpdateEnrollmentCommand(
    [Required] Guid StudentId,
    [Required] Guid SemesterId,
    List<SessionAndCourseOfferingIds> newSessions  
) : IRequest<Result<List<EnrollmentInfo>>>;

 
public record SessionAndCourseOfferingIds(
    Guid SessionId,
    Guid CourseOfferingId
);