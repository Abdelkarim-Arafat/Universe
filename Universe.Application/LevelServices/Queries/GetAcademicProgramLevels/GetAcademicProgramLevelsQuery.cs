using Universe.Application.LevelServices.Dtos;
 
namespace Universe.Application.LevelServices.Queries.GetAcademicProgramLevels;

public record GetAcademicProgramLevelsQuery
(
    [Required]Guid AcademicProgramId,
    FilterRequest Filter
) : IRequest<Result<PaginationList<LevelResponse>>>;
