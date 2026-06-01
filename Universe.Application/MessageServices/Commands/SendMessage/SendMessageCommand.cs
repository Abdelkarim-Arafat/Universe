using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.Message;

namespace Universe.Application.MessageServices.Commands.SendMessage;

public sealed record SendMessageCommand(
    [Required] Guid SenderId,
    [Required] Guid ReceiverId,
    [Required] string Subject,
    [Required] string Body
) : IRequest<Result<MessageResponse>>;