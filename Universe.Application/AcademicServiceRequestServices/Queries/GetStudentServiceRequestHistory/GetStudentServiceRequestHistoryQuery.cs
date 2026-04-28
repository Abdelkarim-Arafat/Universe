using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.AcademicServiceRequestServices.ServiceRequestDtos;

namespace Universe.Application.AcademicServiceRequestServices.Queries.GetStudentServiceRequestHistory;

public record GetStudentServiceRequestHistoryQuery(
    FilterRequest FilterRequest
) : IRequest<Result<PaginationList<ServiceRequestHistoryResponse>>>;