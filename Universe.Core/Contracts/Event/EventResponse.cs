using Universe.Core.Enums;

namespace Universe.Core.Contracts.Event;

public record EventResponse(
    string Id,
    EventType Type,
    DateOnly StartDate,
    DateOnly EndDate
);
