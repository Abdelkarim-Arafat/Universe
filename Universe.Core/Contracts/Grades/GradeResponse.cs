namespace Universe.Core.Contracts.Grades;

public record GradeResponse
(
    Guid Id,
    string Name,
    string Code,
    int MinScore,
    int MaxScore,
    decimal MinGradePoint,
    decimal MaxGradePoint
);
