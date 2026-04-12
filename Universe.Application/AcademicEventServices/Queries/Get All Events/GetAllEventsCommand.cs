using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcademicEventServices.EvenetDtos;

namespace Universe.Application.AcademicEventServices.Queries.Get_All_Events;

public record GetAllEventsCommand(
    [Required] Guid ProgramId,
    [Required] Guid SemesterId,
    FilterRequest FilterRequest
) : IRequest<Result<PaginationList<EventResponse>>>;
