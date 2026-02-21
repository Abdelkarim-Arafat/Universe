using Universe.Application.LevelServices.LevelDtos;
 
namespace Universe.Application.LevelServices.Queries.GetCollegeLevels;

public record GetCollegeLevelsQuery
(
    Guid CollegeId
) : IRequest<Result<List<LevelResponse>>>;
