namespace Universe.Core.Contracts.AcadimicYearAndSemesters;

public record AcademicYearWithSemesterResponse(
    Guid Id,
    string Name,
    DateOnly StartDate,
    DateOnly EndDate,
    List<SemesterResponse> Semesters
);
