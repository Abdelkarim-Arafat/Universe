using Universe.Core.Contracts.Staff;

namespace Universe.Application.StaffServices.Commands.RegisterStaff;

public record RegisterStaffCommand(
    [Required] Guid CollegeId,
    string Name,
    string UserName,
    string Password,
    List<string> Roles,
    string? Email,
    string? PhoneNumber
) : IRequest<Result<StaffWithDetailsResponse>>;
