using Microsoft.EntityFrameworkCore;

namespace Universe.Application.MessageServices.Commands.ReadMessage;

internal class ReadMessageCommandHandler(
    IUnitOfWork unitOfWork,
    IMessageRealtimeService messageRealtimeService,
    INotificationRealtimeService notificationRealtimeService,
    ICacheService cacheService
    ) : IRequestHandler<ReadMessageCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMessageRealtimeService _messageRealtimeService = messageRealtimeService;
    private readonly INotificationRealtimeService _notificationRealtimeService = notificationRealtimeService;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result> Handle(ReadMessageCommand request, CancellationToken cancellationToken)
    {
        var message = await _unitOfWork
            .Repository<Message>()
            .GetQueryable()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.MessageId, cancellationToken);

        if (message is null)
            return Result.Failure(MessageErrors.NotFound);

        if (message.ReceiverId != request.UserId)
            return Result.Failure(MessageErrors.UnAuthorized);

        if (message.IsRead)
            return Result.Success();

        message.IsRead = true;

        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveByTagAsync(MessageCacheKeys.Tags(request.UserId) , cancellationToken);

        await _notificationRealtimeService.SendUnreadCountChangedAsync(
            request.UserId,
            await _unitOfWork.NotificationRepository
                .CountUnSeenNotificationsAsync(request.UserId, cancellationToken),
            cancellationToken
        );

        return Result.Success();
    }
}