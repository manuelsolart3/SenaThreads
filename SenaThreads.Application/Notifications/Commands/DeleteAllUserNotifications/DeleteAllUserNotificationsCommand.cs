using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Notifications.Commands.DeleteAllUserNotifications;
public record DeleteAllUserNotificationsCommand(string UserId) : ICommand;

