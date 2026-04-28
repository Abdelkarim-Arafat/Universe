using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcademicServiceServices.ServiceDtos;

namespace Universe.Application.AcademicServiceServices.Queries.GetAllServices;

public record GetAllServicesQuery(
    [Required] Guid CollegeId,
    FilterRequest FilterRequest
) : IRequest<Result<PaginationList<ServiceResponse>>>;
