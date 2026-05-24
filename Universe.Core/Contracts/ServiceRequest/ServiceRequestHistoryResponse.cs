using Universe.Core.Enums;

namespace Universe.Core.Contracts.ServiceRequest;

public record ServiceRequestHistoryResponse(
    decimal Price,
    string ServiceName,
    string StudentName,
    string StudentCode,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    RequestStatus Status
);