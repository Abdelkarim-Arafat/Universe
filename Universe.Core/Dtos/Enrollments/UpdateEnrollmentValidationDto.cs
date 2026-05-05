using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Enums;

namespace Universe.Core.Dtos.Enrollments;

public record UpdateEnrollmentValidationDto
(
    bool isSemesterExist,
    decimal? minHours,
    decimal? maxHours,
    decimal  registeredHours,
    List<SessionDetailsDto> sessionDetails
);
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
 