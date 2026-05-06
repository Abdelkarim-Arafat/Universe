using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.ServiceRequest;

namespace Universe.Application.AcademicServiceRequestServices.Queries.GetServiceRequestHistory;

public record GetServiceRequestHistoryQuery(
    [Required] Guid CollegeId,
    FilterRequest Filter
) : IRequest<Result<PaginationList<ServiceRequestHistoryResponse>>>;