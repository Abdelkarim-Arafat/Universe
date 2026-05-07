using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.CourseOfferings;

namespace Universe.Application.CourseOfferingServices.Queries.GetCourseOffering;

public record GetCourseOfferingCommand (
    [Required] Guid Id
) : IRequest<Result<CourseOfferingWithDetailsResponse>>;