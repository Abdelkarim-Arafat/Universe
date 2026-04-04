using Universe.Application.CourseServices.Dtos;

namespace Universe.Application.LevelServices.LevelDtos;

public record LevelCoursesResponse
(
    string LevelName,
    List<CourseRegistrationResponse> Courses
);
