using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Domain.Notifications;

namespace SenaThreads.Application.Notifications.Commands.SendNotification;
public record SendNotificationCommand(
    string UserId,
    NotificationType Type,
    string Path,
    string Username,
    string FirstName,
    string LastName,
    string ProfilePictureS3Key
    ) : ICommand;
