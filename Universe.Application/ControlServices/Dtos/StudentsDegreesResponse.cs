namespace Universe.Application.ControlServices.Dtos;

public record StudentsDegreesResponse(
    List<AssessmentHeader> AssessmentHeaders,
    decimal CourseTotalGrade,
    PaginationList<StudentInformation> StudentsInformation
);

public record AssessmentHeader(
    Guid AssessmentId,
    string Name,
    decimal MaxDegree
);

public record StudentInformation(
    Guid StudentId,
    string Name,
    string Code,
    string LevelName,
    int NumberOfFailed,
    decimal TotalDegree,   
    string LetterDegree,   
    List<StudentDegreeValue> StudentDegrees  
);

public record StudentDegreeValue(
    Guid CourseAssessmentId,
    Guid StudentAssessmentId,
    decimal? DegreeValue     
);

