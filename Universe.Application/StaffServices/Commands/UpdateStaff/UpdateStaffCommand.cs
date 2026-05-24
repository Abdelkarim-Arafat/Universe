using Universe.Core.Contracts.Staff;

namespace Universe.Application.StaffServices.Commands.UpdateStaff;

public record UpdateStaffCommand(
    [Required] Guid UserId,
    string Name,
    string UserName,
    List<string> Roles,
    string? Email,
    string? PhoneNumber
) : IRequest<Result<StaffWithDetailsResponse>>;