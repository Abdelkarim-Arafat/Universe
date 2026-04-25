using Microsoft.Data.SqlClient.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcademicEventServices.EvenetDtos;

namespace Universe.Application.AcademicEventServices.Commands.Add_Event;

public record AddEventCommand(
    [Required]Guid SemesterId,
    [Required]Guid ProgramId,
    Core.Enums.EventType Type,
    DateOnly StartDate,
    DateOnly EndDate
) : IRequest<Result<EventResponse>>;
