
using Universe.Application.LevelServices.Dtos;

namespace Universe.Application.LevelServices.Commands.Create;

public record CreateLevelCommand
(
   [Required] Guid AcademicProgramId,
    string Name,
    int MinHours,
    int MaxHours
) : IRequest<Result<LevelResponse>>;
