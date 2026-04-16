
namespace Universe.Application.ControlServices.Commands.AnnounceResult;

public record AnnounceResultCommand
([Required] Guid SemesterId) : IRequest<Result>;

