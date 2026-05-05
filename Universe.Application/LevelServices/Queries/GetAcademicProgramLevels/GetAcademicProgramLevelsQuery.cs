using Universe.Core.Contracts.Level;

 
namespace Universe.Application.LevelServices.Queries.GetAcademicProgramLevels;

public record GetAcademicProgramLevelsQuery (
    [Required] Guid ProgramId,
    FilterRequest Filter
) : IRequest<Result<PaginationList<LevelResponse>>>;
