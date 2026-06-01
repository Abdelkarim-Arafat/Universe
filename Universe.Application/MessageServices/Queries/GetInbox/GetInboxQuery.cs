using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.Message;

namespace Universe.Application.MessageServices.Queries.GetInbox;

public record GetInboxQuery(
    [Required] Guid UserId,
    [Required] FilterRequest Filter
) : IRequest<Result<PaginationList<MessageResponse>>>;