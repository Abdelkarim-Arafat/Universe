namespace Universe.Application.ControlServices.Dtos;

public record UpsertDegreesResponse(
    List<UpdatedStudentScore> UpdatedScores
);

public record UpdatedStudentScore(
    Guid StudentId,
    decimal TotalDegree,   
    string LetterDegree,   
    List<StudentDegreeValue> StudentDegrees
);
