using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.Service;

namespace Universe.Application.AcademicServiceServices.Queries.GetAllServices;

public record GetServicesQuery(
    [Required] Guid CollegeId,
    FilterRequest Filter
) : IRequest<Result<PaginationList<ServiceResponse>>>;
