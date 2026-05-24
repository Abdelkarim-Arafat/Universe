namespace Universe.Application.StudyLoadByLevelServices.Commands.RemoveStudyLoad;

public record RemoveStudyLoadByLevelCommand(
    [Required] Guid ProgramId,
    [Required] Guid Id
) : IRequest<Result>;