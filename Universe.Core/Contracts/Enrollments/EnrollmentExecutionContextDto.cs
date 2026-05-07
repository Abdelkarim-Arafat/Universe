using Universe.Core.Entities;

namespace Universe.Core.Contracts.Enrollments;

public record EnrollmentExecutionContextDto(
    List<Enrollment> ExistingEnrollments,
    ILookup<Guid, Guid> IncomingAssessmentsLookup 
);
