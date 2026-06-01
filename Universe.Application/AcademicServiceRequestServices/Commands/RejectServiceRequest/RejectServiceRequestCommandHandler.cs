using Universe.Core.Contracts.Notification;
using Universe.Core.Enums;

namespace Universe.Application.AcademicServiceRequestServices.Commands.RejectServiceRequest;

internal class RejectServiceRequestCommandHandler(
    IUnitOfWork unitOfWork,
    IPayPalService paypal,
    ICacheService cacheService,
    INotificationRealtimeService notificationRealtimeService
    ) : IRequestHandler<RejectServiceRequestCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPayPalService _paypal = paypal;
    private readonly ICacheService _cacheService = cacheService;
    private readonly INotificationRealtimeService _notificationRealtimeService = notificationRealtimeService;

    public async Task<Result> Handle(RejectServiceRequestCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.ServiceRepository
            .GetRequestByIdAsync(request.RequestId, cancellationToken) is not { } serviceRequest
            ) return Result.Failure(ServiceErrors.RequestNotFound);

        var payment = await _unitOfWork.PaymentRepository
            .GetByIdAsync(serviceRequest.PaymentId, cancellationToken);

        if (await _paypal.RefundPaymentAsync(payment.CaptureId) is false
            ) return Result.Failure(PaymentErrors.FaildRefund);

        payment.Status = PaymentStatus.Refunded;
        serviceRequest.Status = RequestStatus.Rejected;
        serviceRequest.UpdatedAt = DateTime.UtcNow;

        var notification = new Notification
        {
            UserId = serviceRequest.StudentId,
            Type = NotificationType.ServiceRequestRejected,
            Title = "Service Request Rejected",
            Body = $"Your service request has been rejected.",
            RelatedId = serviceRequest.Id
        };

        await _unitOfWork.Repository<Notification>().AddAsync(notification, cancellationToken);

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
