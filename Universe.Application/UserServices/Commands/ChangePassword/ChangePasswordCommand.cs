
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Universe.Application.UserServices.Commands.ChangePassword;

public record ChangePasswordCommand (
    [Required] Guid UserId,
    string CurrentPassword,
    [MinLength(8)]string NewPassword
) : IRequest<Result>;