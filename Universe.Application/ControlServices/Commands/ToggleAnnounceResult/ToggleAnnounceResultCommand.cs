namespace Universe.Application.ControlServices.Commands.ToggleAnnounceResult;

public record ToggleAnnounceResultCommand
([Required] Guid SemesterId, Guid ProgramId) : IRequest<Result>;

