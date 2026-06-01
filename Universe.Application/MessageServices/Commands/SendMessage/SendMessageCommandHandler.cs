using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Core.Contracts.Message;
using Universe.Core.Contracts.Notification;
using Universe.Core.Enums;

namespace Universe.Application.MessageServices.Commands.SendMessage;

internal class SendMessageCommandHandler(
    UserManager<ApplicationUser> userManager,
    IUnitOfWork unitOfWork,
    IMessageRealtimeService messageRealtimeService,
    INotificationRealtimeService notificationRealtimeService,
    ICacheService cacheService
    ) : IRequestHandler<SendMessageCommand, Result<MessageResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMessageRealtimeService _messageRealtimeService = messageRealtimeService;
    private readonly INotificationRealtimeService _notificationRealtimeService = notificationRealtimeService;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<MessageResponse>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        if(await _userManager.Users
            .FirstOrDefaultAsync(x => x.Id == request.SenderId && !x.IsDeleted, cancellationToken) is not { } sender
            ) return Result.Failure<MessageResponse>(UserErrors.UserNotFound);

        if (!await _userManager.Users
            .AnyAsync(x => x.Id == request.ReceiverId && !x.IsDeleted, cancellationToken)
            ) return Result.Failure<MessageResponse>(UserErrors.UserNotFound);

        var message = new Message
        {
            SenderId = request.SenderId,
            ReceiverId = request.ReceiverId,
            Subject = request.Subject,
            Body = request.Body
        };

        var notification = new Notification
        {
            UserId = message.ReceiverId,
            Type = NotificationType.Message,
            Title = "New Message Received",
            Body = $"You have received a new message from {sender.Name}",
            RelatedId = message.Id
        };

        await unitOfWork.Repository<Message>().AddAsync(message, cancellationToken);
        await unitOfWork.Repository<Notification>().AddAsync(notification, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveByTagAsync(MessageCacheKeys.Tags(message.SenderId), cancellationToken);
        await _cacheService.RemoveByTagAsync(MessageCacheKeys.Tags(message.ReceiverId), cancellationToken);
        await _cacheService.RemoveByTagAsync(NotificationCacheKeys.Tags(message.ReceiverId), cancellationToken);

        var response = new MessageResponse(
            message.Id,
            message.Subject,
            message.Body,
            message.SenderId,
            sender.Name,
            message.CreatedAt,
            message.IsRead
        );

        await _messageRealtimeService.SendMessageReceivedAsync(request.ReceiverId, response , cancellationToken);

        await _notificationRealtimeService.SendNotificationReceivedAsync(
            message.ReceiverId,
            notification.Adapt<NotificationResponse>(),
            cancellationToken
        );

        await _notificationRealtimeService.SendUnreadCountChangedAsync(
            message.ReceiverId,
            await _unitOfWork.NotificationRepository
                .CountUnSeenNotificationsAsync(message.ReceiverId, cancellationToken),
            cancellationToken
        );

        return Result.Success(response);
    }
}
