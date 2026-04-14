using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.ControlServices.Dtos;

public record GetCourseOfferingsControlStatisticsResponse(
    Guid LevelId,
    string LevelName,
    List<CourseOfferingStatisticsResponse> Offerings
);

public record CourseOfferingStatisticsResponse(
    Guid CourseOfferingId,
    string CourseName,
    string CourseCode,
    int NumberOfRegisteredStudents,
    int NumberOfStudentsWithMissDegrees,
    bool IsOpenForControl
);