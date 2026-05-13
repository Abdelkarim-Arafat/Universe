using Universe.Core.Enums;

namespace Universe.Core.Contracts.Enrollments;

public record SessionDetailsDto
(
        Guid sessionId,
        Guid courseOfferingId,
        Enums.DayOfWeek day,
        TimeOnly startTime,
        TimeOnly endTime,
        SessionType type,
        int groupNumber,
        int capacity,
        int occupiedSeats
);
 