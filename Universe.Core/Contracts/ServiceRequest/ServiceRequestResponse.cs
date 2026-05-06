using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Contracts.ServiceRequest;

public record ServiceRequestResponse(
    Guid Id,
    decimal Price,
    string ServiceName,
    string StudentName,
    string StudentCode,
    DateTime CreatedAt
);