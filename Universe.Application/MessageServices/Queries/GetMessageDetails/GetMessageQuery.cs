using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.Message;

namespace Universe.Application.MessageServices.Queries.GetMessageDetails;

public record GetMessageQuery (
    [Required] Guid UserId,
    [Required] Guid MessageId
) : IRequest<Result<MessageWithRepliesResponse>>;