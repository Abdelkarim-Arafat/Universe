namespace Universe.Application.ControlServices.Commands.ToggleAnnounceResult;

public record ToggleAnnounceResultCommand
([Required] Guid SemesterId) : IRequest<Result>;

