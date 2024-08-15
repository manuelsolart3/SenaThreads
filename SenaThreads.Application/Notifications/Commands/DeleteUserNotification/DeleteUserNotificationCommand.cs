using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Notifications.Commands.DeleteUserNotification;
public record DeleteUserNotificationCommand(
    Guid notificationId,
    string userId) : ICommand;
