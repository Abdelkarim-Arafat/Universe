using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.AcademicServiceRequestServices.ServiceRequestDtos;

public record ServiceRequestResponse(
    string Id,
    decimal Price,
    string ServiceName,
    string StudentName,
    string StudentCode,
    DateTime CreatedAt
);