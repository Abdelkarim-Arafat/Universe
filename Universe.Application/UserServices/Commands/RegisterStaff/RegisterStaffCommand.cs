using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.User;

namespace Universe.Application.UserServices.Commands.RegisterStaff;

public record RegisterStaffCommand(
    [Required] Guid CollegeId,
    string Name,
    string UserName,
    string Password,
    List<string> Roles,
    string? Email,
    string? PhoneNumber
) : IRequest<Result<StuffWithDetailsResponse>>;
