using Microsoft.EntityFrameworkCore;

namespace Universe.Application.NotificationServices.Commands.MarkNotificationAsSeen;

internal class MarkNotificationAsSeenCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService,
    INotificationRealtimeService notificationRealtimeService
    ) : IRequestHandler<MarkNotificationAsSeenCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    private readonly INotificationRealtimeService _notificationRealtimeService = notificationRealtimeService;
    public async Task<Result> Handle(MarkNotificationAsSeenCommand request, CancellationToken cancellationToken)
    {
        var notification = await _unitOfWork
            .Repository<Notification>()
            .GetQueryable()
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == request.NotificationId &&
                     x.UserId == request.UserId,
                cancellationToken
            );

        if (notification is null)
            return Result.Failure(NotificationErrors.NotFound);

        if (notification.IsSeen)
            return Result.Success();

        notification.IsSeen = true;

        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveByTagAsync(NotificationCacheKeys.Tags(request.UserId) , cancellationToken);

        var unreadCount = await _unitOfWork.NotificationRepository
            .CountUnSeenNotificationsAsync(request.UserId, cancellationToken);

        await _notificationRealtimeService.SendUnreadCountChangedAsync(request.UserId, unreadCount , cancellationToken);

        return Result.Success();
    }
}