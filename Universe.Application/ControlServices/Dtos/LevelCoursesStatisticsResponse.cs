using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.ControlServices.Dtos;

public record LevelCoursesStatisticsResponse(
    Guid LevelId,
    string LevelName,
    List<CourseOfferingStatisticsResponse> Offerings
);

public record CourseOfferingStatisticsResponse(
    Guid CourseOfferingId,
    string CourseName,
    string CourseCode,
    int NumberOfRegisteredStudents,
    int NumberOfStudentsWithMissDegrees
);