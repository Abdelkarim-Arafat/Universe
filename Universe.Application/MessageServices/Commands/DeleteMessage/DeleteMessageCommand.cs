using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.MessageServices.Commands.DeleteMessage;

public sealed record DeleteMessageCommand(
    [Required] Guid MessageId,
    [Required] Guid UserId
) : IRequest<Result>;