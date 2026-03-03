using Universe.Application.LevelServices.LevelDtos;
 
namespace Universe.Application.LevelServices.Queries.GetAcademicProgramLevels;

public record GetCollegeLevelsQuery
(
    [Required]Guid AcademicProgramId,
    FilterRequest Filter
) : IRequest<Result<PaginationList<LevelResponse>>>;
