using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.UserServices.Commands.UpdateEmail;

public record UpdateEmailCommand(
    [Required] Guid UserId,
    [Required, EmailAddress] string Email
) : IRequest<Result>;