using Universe.Application.EnrollmentServices.Dtos;
using Universe.Core.Dtos.Enrollments;

namespace Universe.Application.EnrollmentServices.Commands.Update;

public record UpdateEnrollmentCommand(
    [Required] Guid StudentId,
    [Required] Guid SemesterId,
    List<SessionAndCourseOfferingIds> newSessions  
) : IRequest<Result<List<StudentExistingEnrollment>>>;

 
public record SessionAndCourseOfferingIds(
    Guid SessionId,
    Guid CourseOfferingId
);