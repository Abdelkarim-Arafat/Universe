using Universe.Core.Contracts.AcademicProgram;

namespace Universe.Application.AcademicProgramServices.Query.GetAcademicProgram;

public record GetAcademicProgramQuery(
    [Required] Guid Id
) : IRequest<Result<AcademicProgramResponse>>;
