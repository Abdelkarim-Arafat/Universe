using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcademicServiceRequestServices.ServiceRequestDtos;

namespace Universe.Application.AcademicServiceRequestServices.Queries.GetServiceRequestHistory;

public record GetServiceRequestHistoryQuery(
    [Required] Guid CollegeId,
    FilterRequest FilterRequest
) : IRequest<Result<PaginationList<ServiceRequestHistoryResponse>>>;