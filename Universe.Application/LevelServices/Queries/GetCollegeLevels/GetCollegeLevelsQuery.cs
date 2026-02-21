using Universe.Application.LevelServices.LevelDtos;
 
namespace Universe.Application.LevelServices.Queries.GetCollegeLevels;

public record GetCollegeLevelsQuery
(
    [Required]Guid CollegeId,
    FilterRequest Filter
) : IRequest<Result<PaginationList<LevelResponse>>>;
