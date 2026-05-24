namespace Universe.Core.Contracts.ServiceRequest;

public record ServiceRequestResponse(
    Guid Id,
    decimal Price,
    string ServiceName,
    string StudentName,
    string StudentCode,
    DateTime CreatedAt
);