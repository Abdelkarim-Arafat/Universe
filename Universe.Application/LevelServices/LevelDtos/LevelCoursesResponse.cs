
using Universe.Core.Contracts.Course;

namespace Universe.Application.LevelServices.LevelDtos;

public record LevelCoursesResponse
(
    string LevelName,
    List<CourseRegistrationResponse> Courses
);
