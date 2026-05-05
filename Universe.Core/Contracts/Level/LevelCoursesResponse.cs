
using Universe.Core.Contracts.Course;

namespace Universe.Core.Contracts.Level;

public record LevelCoursesResponse(
    string LevelName,
    List<CourseRegistrationResponse> Courses
);
