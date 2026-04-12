using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.UserServices.UserDtos;

namespace Universe.Application.UserServices.Commands.UpdateStuff;

public record UpdateStuffCommand(
    [Required] Guid UserId,
    string Name,
    string UserName,
    List<string> Roles,
    string? Email,
    string? PhoneNumber
) : IRequest<Result<StaffResponse>>;