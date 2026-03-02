
using Universe.Application.AcademicProgramServices.AcademicProgramDtos;

namespace Universe.Application.AcademicProgramServices.Query.GetAcademicPrograms;

public record GetAcademicProgramsCommand(
    [Required]Guid CollegeId,
    FilterRequest Filter
) : IRequest<Result<PaginationList<AcademicProgramResponse>>>;