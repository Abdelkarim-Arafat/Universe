using Universe.Application.UserServices.UserDtos;
using Universe.Core.Contracts.Level;

namespace Universe.Application.EnrollmentServices.Dtos;

public record EnrollmentPageResponse
(
    StudentInfoResponse Student,
    LevelCoursesResponse LevelInfo,
    List<EnrollmentInfo> EnrollmentInfos
);
