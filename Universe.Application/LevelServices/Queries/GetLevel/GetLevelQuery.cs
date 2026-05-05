
using Universe.Core.Contracts.Level;

namespace Universe.Application.LevelServices.Queries.GetLevel;

public record GetLevelQuery (
    [Required] Guid ProgramId,
    [Required] Guid Id
) : IRequest<Result<LevelResponse>>;
