using Universe.Application.LevelServices.Dtos;

namespace Universe.Application.LevelServices.Queries.GetLevel;

public record GetLevelQuery
(
    [Required]Guid Id
) : IRequest<Result<LevelResponse>>;
