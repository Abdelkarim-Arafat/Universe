namespace Universe.Application.AcademicServiceServices.Commands.Toggle_Service;

public record ToggleServiceCommand(
    [Required] Guid Id
) : IRequest<Result>;