using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.AcademicServiceServices.Queries.GetStudentServiceRequestHistory;

public record GetStudentServiceRequestHistoryQuery(
    FilterRequest FilterRequest
) : IRequest<Result<PaginationList<ServiceRequestHistoryResponse>>>;