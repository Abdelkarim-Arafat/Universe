using Universe.Application.LevelServices.LevelDtos;
using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.EnrollmentServices.Dtos;

public record EnrollmentPageResponse
(
    StudentInfoResponse Student,
    LevelCoursesResponse LevelInfo,
    List<EnrollmentInfo> EnrollmentInfos
);
