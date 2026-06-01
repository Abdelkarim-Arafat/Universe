using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.Message;
using Universe.Core.Contracts.Notification;
using Universe.Core.Enums;

namespace Universe.Application.MessageServices.Commands.ReplyMessage;

internal class ReplyMessageCommandHandler(
    IUnitOfWork unitOfWork,
    IMessageRealtimeService messageRealTimeService,
    INotificationRealtimeService notificationRealtimeService,
    ICacheService cacheService
    ) : IRequestHandler<ReplyMessageCommand, Result<MessageReplyResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMessageRealtimeService _messageRealTimeService = messageRealTimeService;
    private readonly INotificationRealtimeService _notificationRealtimeService = notificationRealtimeService;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<MessageReplyResponse>> Handle(ReplyMessageCommand request, CancellationToken cancellationToken)
    {
        var parentMessage = await _unitOfWork
            .Repository<Message>()
            .GetQueryable()
            .AsNoTracking()
            .Include(x => x.Sender)
            .Include(x => x.Receiver)
            .FirstOrDefaultAsync(x => x.Id == request.ParentMessageId, cancellationToken);

        if (parentMessage is null)
            return Result.Failure<MessageReplyResponse>(MessageErrors.NotFound);

        if (request.SenderId != parentMessage.SenderId && request.SenderId != parentMessage.ReceiverId)
        {
            return Result.Failure<MessageReplyResponse>(MessageErrors.UnAuthorized);
        }

        var receiverId =
            request.SenderId == parentMessage.SenderId
                ? parentMessage.ReceiverId
                : parentMessage.SenderId;

        var senderName = request.SenderId == parentMessage.SenderId
            ? parentMessage.Sender.Name
            : parentMessage.Receiver.Name;

        var reply = new Message
        {
            SenderId = request.SenderId,
            ReceiverId = receiverId,
            ParentMessageId = parentMessage.Id,
            Subject = $"Re: {parentMessage.Subject}",
            Body = request.Body
        };

        var notification = new Notification
        {
            UserId = reply.ReceiverId,
            Type = NotificationType.Message,
            Title = "New Reply",
            Body = $"{senderName} replied to a message.",
            RelatedId = parentMessage.Id
        };

        await _unitOfWork.Repository<Message>().AddAsync(reply, cancellationToken);
        await _unitOfWork.Repository<Notification>().AddAsync(notification, cancellationToken);

        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveByTagAsync(MessageCacheKeys.Tags(reply.SenderId), cancellationToken);
        await _cacheService.RemoveByTagAsync(MessageCacheKeys.Tags(reply.ReceiverId), cancellationToken);
        await _cacheService.RemoveByTagAsync(NotificationCacheKeys.Tags(reply.ReceiverId), cancellationToken);

        var response = new MessageReplyResponse(
            reply.Id,
            reply.SenderId,
            reply.ReceiverId,
            reply.ParentMessageId,
            senderName,
            reply.Body,
            reply.IsRead,
            reply.CreatedAt
        );

        await _messageRealTimeService.SendMessageReplyAsync(reply.ReceiverId, response, cancellationToken);

        await _notificationRealtimeService.SendNotificationReceivedAsync(
            notification.UserId,
            notification.Adapt<NotificationResponse>(),
            cancellationToken
        );

        await _notificationRealtimeService.SendUnreadCountChangedAsync(
            reply.ReceiverId,
            await _unitOfWork.NotificationRepository
                .CountUnSeenNotificationsAsync(reply.ReceiverId, cancellationToken),
            cancellationToken
        );

        return Result.Success(response);
    }
}
