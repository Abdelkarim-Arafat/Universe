namespace Universe.Core.Contracts.Student;

public record GraduationDetailsResponse(
    decimal GPA,
    string GraduationYear,
    string GraduationSemester,
    string GraduationProjectName
);