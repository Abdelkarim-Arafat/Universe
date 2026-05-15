using Universe.Core.Entities;

namespace Universe.Core.Contracts.Enrollments;

public record ExistingEnrollmentDto(
    List<Enrollment> ExistingEnrollments,
    ILookup<Guid, Guid> IncomingAssessmentsLookup 
);
