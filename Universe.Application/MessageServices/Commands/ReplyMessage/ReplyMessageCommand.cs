using Microsoft.AspNetCore.Authentication.BearerToken;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.Message;

namespace Universe.Application.MessageServices.Commands.ReplyMessage;

public sealed record ReplyMessageCommand(
    [Required] Guid SenderId,
    [Required] Guid ParentMessageId,
    [Required] string Body
) : IRequest<Result<MessageReplyResponse>>;