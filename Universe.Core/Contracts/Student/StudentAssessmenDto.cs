using Universe.Core.Entities;

namespace Universe.Core.Contracts.Student;

public record StudentAssessmenDto(
    decimal MaxScore,
    StudentAssessment? Assessment
);


