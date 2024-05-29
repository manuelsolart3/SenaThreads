using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Notifications;

namespace SenaThreads.Application.Notifications.Commands.SendNotification;
public record SendNotificationCommand(
    string UserId,
    NotificationType Type,
    string Path) : ICommand;
