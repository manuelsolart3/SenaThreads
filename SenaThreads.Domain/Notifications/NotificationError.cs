using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Domain.Notifications;
public static class NotificationError
{
    public static readonly Error NotificationNotFound = new(
    "Notification.NotFound",
    "The notification was not found"
);

    public static readonly Error NotificationAlreadyRead = new(
        "Notification.AlreadyRead",
        "The notification has already been marked as read"
    );
}
