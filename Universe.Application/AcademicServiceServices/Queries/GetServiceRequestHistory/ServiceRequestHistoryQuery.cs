using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.AcademicServiceServices.Queries.GetServiceRequestHistory;

public record GetServiceRequestHistoryQuery(
    [Required] Guid CollegeId,
    FilterRequest FilterRequest
) : IRequest<Result<PaginationList<ServiceRequestHistoryResponse>>>;