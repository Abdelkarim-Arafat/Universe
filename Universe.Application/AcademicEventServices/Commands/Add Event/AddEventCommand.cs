

using Universe.Core.Contracts.Event;

namespace Universe.Application.AcademicEventServices.Commands.Add_Event;

public record AddEventCommand(
    [Required]Guid SemesterId,
    [Required]Guid ProgramId,
    Core.Enums.EventType Type,
    DateOnly StartDate,
    DateOnly EndDate
) : IRequest<Result<EventResponse>>;
