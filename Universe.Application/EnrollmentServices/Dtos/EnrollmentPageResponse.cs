using Universe.Application.UserServices.UserDtos;
using Universe.Core.Dtos.Enrollments;

namespace Universe.Application.EnrollmentServices.Dtos;

public record EnrollmentPageResponse(
    StudentInfoResponse Student,
    LevelRegistrationCatalogDto LevelInfo,
    List<StudentExistingEnrollment> EnrollmentInfos 
);
