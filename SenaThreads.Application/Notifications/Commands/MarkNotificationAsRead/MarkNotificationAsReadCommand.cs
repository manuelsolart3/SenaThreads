using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Notifications.Commands.MarkNotificationAsRead;
public record MarkNotificationAsReadCommand(
    Guid NotificationId,
    string UserId) :ICommand;
