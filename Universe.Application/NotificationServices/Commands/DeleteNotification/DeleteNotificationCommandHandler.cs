namespace Universe.Application.NotificationServices.Commands.DeleteNotification;

internal class DeleteNotificationCommandHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService
) : IRequestHandler<DeleteNotificationCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = await _unitOfWork.NotificationRepository
            .GetByIdAsync(request.NotificationId, request.UserId, cancellationToken);

        if (notification is null)
            return Result.Failure(NotificationErrors.NotFound);

        _unitOfWork.Repository<Notification>().DeletePermanently(notification);

        await _unitOfWork.CompleteAsync(cancellationToken);

        await _cacheService.RemoveByTagAsync(NotificationCacheKeys.Tags(notification.UserId), cancellationToken);

        return Result.Success();
    }
}