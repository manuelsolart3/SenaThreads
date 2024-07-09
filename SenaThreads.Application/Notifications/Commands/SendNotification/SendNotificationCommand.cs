using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Domain.Notifications;

namespace SenaThreads.Application.Notifications.Commands.SendNotification;
public record SendNotificationCommand(
    string NotifierUserId,
    string ReceiverUserId,
    NotificationType Type,
    string Path
) : ICommand;
