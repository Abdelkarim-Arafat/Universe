using Universe.Core.Enums;

namespace Universe.Application.TeachingSessionServices.SessionDtos;

public record SessionOptionResponse
(
Guid SessionId,
string InstructorName,
SessionType Type,
int GroupNumber,
Core.Enums.DayOfWeek Day,
TimeOnly Start,
TimeOnly End,
int AvailableSeats
);
