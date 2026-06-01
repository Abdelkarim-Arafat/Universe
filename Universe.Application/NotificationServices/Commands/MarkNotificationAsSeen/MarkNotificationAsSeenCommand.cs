using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.NotificationServices.Commands.MarkNotificationAsSeen;

public sealed record MarkNotificationAsSeenCommand(
    Guid NotificationId,
    Guid UserId
) : IRequest<Result>;