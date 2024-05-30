using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Notifications.Commands.MarkNotificationAsRead;
public record MarkNotificationAsReadCommand(
    Guid NotificationId,
    string UserId) :ICommand;
