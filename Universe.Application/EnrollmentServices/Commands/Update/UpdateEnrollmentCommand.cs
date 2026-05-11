using Universe.Core.Contracts.Enrollments;

namespace Universe.Application.EnrollmentServices.Commands.Update;

public record UpdateEnrollmentCommand(
     Guid StudentId,
     Guid SemesterId,
    List<SessionAndCourseOfferingIds> newSessions  
) : IRequest<Result<List<StudentExistingEnrollment>>>;

 
public record SessionAndCourseOfferingIds(
    Guid SessionId,
    Guid CourseOfferingId
);