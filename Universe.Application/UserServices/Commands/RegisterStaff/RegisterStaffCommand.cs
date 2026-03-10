using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.UserServices.Commands.RegisterStaff;

public record RegisterStaffCommand(
    [Required] Guid CollegeId,
    string Name,
    string UserName,
    string Password,
    string Role
) : IRequest<Result<StaffResponse>>;
