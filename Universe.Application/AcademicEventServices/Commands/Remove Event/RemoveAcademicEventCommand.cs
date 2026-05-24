namespace Universe.Application.AcademicEventServices.Commands.Remove_Event;

public record RemoveAcademicEventCommand(
    [Required] Guid Id
) : IRequest<Result>;
