using Microsoft.Data.SqlClient.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcademicEventServices.EvenetDtos;

namespace Universe.Application.AcademicEventServices.Commands.Add_Event;

public record AddEventCommand(
    Guid SemesterId,
    Guid ProgramId,
    Core.Enums.EventType Type,
    DateTime StartDate,
    DateTime EndDate
) : IRequest<Result<EventResponse>>;
