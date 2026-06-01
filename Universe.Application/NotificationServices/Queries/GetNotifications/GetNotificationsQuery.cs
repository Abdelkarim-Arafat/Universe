using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.Notification;

namespace Universe.Application.NotificationServices.Queries.GetNotifications;

public record GetNotificationsQuery(
    Guid UserId,
    FilterRequest Filter
) : IRequest<Result<PaginationList<NotificationResponse>>>;