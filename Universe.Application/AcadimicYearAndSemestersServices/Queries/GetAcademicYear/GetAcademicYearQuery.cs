using Universe.Core.Contracts.AcadimicYearAndSemesters;

namespace Universe.Application.AcadimicYearAndSemestersServices.Queries.GetAcademicYear;

public record GetAcademicYearQuery(
    [Required] Guid Id
) : IRequest<Result<AcademicYearWithSemesterResponse>>;