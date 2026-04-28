using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcademicServiceRequestServices.ServiceRequestDtos;

namespace Universe.Application.AcademicServiceRequestServices.Queries.GetAllServiceRequests;

public record GetAllServiceRequestsQuery(
    [Required] Guid CollegeId,
    FilterRequest FilterRequest
) : IRequest<Result<PaginationList<ServiceRequestResponse>>>;