
using Universe.Core.Contracts.AcademicProgram;

namespace Universe.Application.AcademicProgramServices.Query.GetAcademicPrograms;

public record GetAcademicProgramsQuery(
    [Required]Guid CollegeId,
    FilterRequest Filter
) : IRequest<Result<PaginationList<GetAcademicProgramsResponse>>>;