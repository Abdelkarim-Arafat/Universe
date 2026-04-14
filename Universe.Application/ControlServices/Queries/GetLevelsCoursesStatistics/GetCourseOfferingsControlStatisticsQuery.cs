using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.ControlServices.Dtos;

namespace Universe.Application.ControlServices.Queries;

public record GetCourseOfferingsControlStatisticsQuery(
    [Required] Guid SemesterId,
    [Required] Guid ProgramId
) : IRequest<Result<List<GetCourseOfferingsControlStatisticsResponse>>>;