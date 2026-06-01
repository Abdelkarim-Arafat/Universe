using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Entities;

namespace Universe.Core.Interfaces.Repositories;

public interface INotificationRepository
{
    Task<Notification?> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken);
    Task<int> CountUnSeenNotificationsAsync(Guid userId, CancellationToken cancellationToken);
}
