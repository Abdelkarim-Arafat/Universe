using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.AcademicServiceServices.Queries.GetAllServiceRequests;

public record GetAllServiceRequestsQuery(
    [Required] Guid CollegeId,
    FilterRequest FilterRequest
) : IRequest<Result<PaginationList<ServiceRequestResponse>>>;