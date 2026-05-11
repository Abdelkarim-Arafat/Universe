using Universe.Core.Contracts.User;
using Universe.Core.Contracts.Enrollments;

namespace Universe.Application.EnrollmentServices.Dtos;

public record EnrollmentPageResponse(
    StudentInfoResponse Student,
    LevelRegistrationCatalogDto LevelInfo,
    List<StudentExistingEnrollment> EnrollmentInfos 
);
