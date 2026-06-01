
namespace Universe.Application.NotificationServices.Commands.DeleteNotification;

public record DeleteNotificationCommand(
    [Required] Guid NotificationId,
    [Required] Guid UserId
) : IRequest<Result>;