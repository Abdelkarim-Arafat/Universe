using Universe.Application.UserServices.UserDtos;
using Universe.Core.Contracts.Enrollments;

namespace Universe.Application.EnrollmentServices.Dtos;

public record EnrollmentPageResponse(
    StudentInfoResponse Student,
    LevelRegistrationCatalogDto LevelInfo,
    List<StudentExistingEnrollment> EnrollmentInfos 
);
