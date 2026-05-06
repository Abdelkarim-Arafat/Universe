using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.CourseOffering;

namespace Universe.Application.CourseOfferingServices.Query.GetCourseOffering;

public record GetCourseOfferingQuery (
    [Required] Guid Id
) : IRequest<Result<CourseOfferingWithDetailsResponse>>;