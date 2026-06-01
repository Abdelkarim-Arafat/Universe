using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Universe.Core.Entities;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Infrastructure.Repositories;

internal class NotificationRepository(ApplicationDbContext context) : INotificationRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Notification?> GetByIdAsync(Guid id , Guid userId , CancellationToken cancellationToken)
        => await _context.Notifications
            .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId, cancellationToken);

    public async Task<int> CountUnSeenNotificationsAsync(Guid userId, CancellationToken cancellationToken)
        => await _context.Notifications
            .CountAsync(x => x.UserId == userId && !x.IsSeen, cancellationToken);
}
