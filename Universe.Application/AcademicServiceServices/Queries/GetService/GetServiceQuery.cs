using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.Service;

namespace Universe.Application.AcademicServiceServices.Queries.GetService;

public record GetServiceQuery(
    Guid CollegeId,
    Guid ServiceId
) : IRequest<Result<ServiceResponse>>;