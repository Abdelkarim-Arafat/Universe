namespace Universe.Core.Contracts.Control;
public record StudentInformationResponse(
    Guid StudentId,
    string Name,
    string Code,
    string StudentLevelName,
    int NumberOfFailed,
    decimal TotalDegree,   
    string LetterDegree,   
    List<StudentDegreeValue> StudentDegrees  
);

public record StudentDegreeValue(
    Guid CourseAssessmentId,
    decimal? DegreeValue     
);

