using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.NotificationServices.Queries.GetUnreadNotificationsCount;

public sealed record GetUnreadNotificationsCountQuery(
    Guid UserId
) : IRequest<Result<int>>;