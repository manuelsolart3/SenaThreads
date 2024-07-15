using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Domain.Notifications;

namespace SenaThreads.Application.Notifications.Commands.SendNotification;
public record SendNotificationCommand(
    string notifierUserId,
    string receiverUserId,
    NotificationType type,
    string path
) : ICommand;
