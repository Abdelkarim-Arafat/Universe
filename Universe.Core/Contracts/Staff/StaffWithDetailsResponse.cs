namespace Universe.Core.Contracts.Staff;

public record StaffWithDetailsResponse(
    string Id,
    string Name,
    List<string> Roles,
    string UserName,
    string? Email,
    string? PhoneNumber
);