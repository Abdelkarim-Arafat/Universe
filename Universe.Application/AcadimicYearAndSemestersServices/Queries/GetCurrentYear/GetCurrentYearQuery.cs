using Universe.Core.Contracts.AcadimicYearAndSemesters;

namespace Universe.Application.AcadimicYearAndSemestersServices.Queries.GetCurrentYear;

public record GetCurrentYearQuery(
    [Required] Guid CollegeId
) : IRequest<Result<AcademicYearResponse>>;