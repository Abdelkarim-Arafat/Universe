namespace Universe.Application.StaffServices.Commands.RemoveStaff;

public record RemoveStaffCommand(
    [Required] Guid Id
) : IRequest<Result>;
