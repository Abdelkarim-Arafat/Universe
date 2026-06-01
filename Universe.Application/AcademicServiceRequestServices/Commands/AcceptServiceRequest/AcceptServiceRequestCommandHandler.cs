using Universe.Core.Contracts.Notification;
using Universe.Core.Enums;

namespace Universe.Application.AcademicServiceRequestServices.Commands.AcceptServiceRequest;

public class AcceptServiceRequestCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService,
    INotificationRealtimeService notificationRealtimeService
    ) : IRequestHandler<AcceptServiceRequestCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    private readonly INotificationRealtimeService _notificationRealtimeService = notificationRealtimeService;

    public async Task<Result> Handle(AcceptServiceRequestCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.ServiceRepository
            .GetRequestByIdAsync(request.RequestId, cancellationToken) is not { } serviceRequest
            ) return Result.Failure(ServiceErrors.RequestNotFound);

        serviceRequest.Status = RequestStatus.Ready;
        serviceRequest.UpdatedAt = DateTime.UtcNow;

        var notification = new Notification
        {
            UserId = serviceRequest.StudentId,
            Type = NotificationType.ServiceRequestAccepted,
            Title = "Service Request Accepted",
            Body = $"Your service request has been accepted.",
            RelatedId = serviceRequest.Id
        };

        await _unitOfWork.Repository<Notification>().AddAsync(notification, cancellationToken);
        _unitOfWork.Repository<ServiceRequest>().Update(serviceRequest);
        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveByTagAsync(ServiceRequestCacheKeys.Tags(request.CollegeId), cancellationToken);
        await _cacheService.RemoveByTagAsync(NotificationCacheKeys.Tags(serviceRequest.StudentId), cancellationToken);

        await _notificationRealtimeService.SendNotificationReceivedAsync(
            serviceRequest.StudentId,
            notification.Adapt<NotificationResponse>(),
            cancellationToken
        );

        await _notificationRealtimeService.SendUnreadCountChangedAsync(
            serviceRequest.StudentId,
            await _unitOfWork.NotificationRepository
            .CountUnSeenNotificationsAsync(serviceRequest.StudentId, cancellationToken),
            cancellationToken
        );

        

        return Result.Success();
    }
}
