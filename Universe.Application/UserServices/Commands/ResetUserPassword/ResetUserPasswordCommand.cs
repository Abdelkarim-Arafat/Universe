using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.UserServices.Commands.ResetUserPassword;

public record ResetUserPasswordCommand(
    [Required] string UserName,
    [Required] string NewPassword
) : IRequest<Result>;