using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Enums;

namespace Universe.Application.AcademicServiceServices;

public record ServiceRequestHistoryResponse(
    decimal Price,
    string ServiceName,
    string StudentName,
    string StudentCode,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    RequestStatus Status
);