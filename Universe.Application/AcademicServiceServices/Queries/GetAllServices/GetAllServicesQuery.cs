using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.AcademicServiceServices.Queries.GetAllServices;

public record GetAllServicesQuery(
    [Required] Guid CollegeId,
    FilterRequest FilterRequest
) : IRequest<Result<PaginationList<ServiceResponse>>>;
