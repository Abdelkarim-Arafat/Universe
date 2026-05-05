using Universe.Core.Entities;

namespace Universe.Core.Dtos.Enrollments;

public record EnrollmentExecutionContextDto(
    List<Enrollment> ExistingEnrollments,
    ILookup<Guid, Guid> IncomingAssessmentsLookup 
);
