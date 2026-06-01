using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.MessageServices.Commands.ReadMessage;

public record ReadMessageCommand(
    [Required] Guid MessageId,
    [Required] Guid UserId
) : IRequest<Result>;