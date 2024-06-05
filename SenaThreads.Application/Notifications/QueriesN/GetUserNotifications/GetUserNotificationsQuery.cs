using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Notifications;

namespace SenaThreads.Application.Notifications.QueriesN.GetUserNotifications;
public record GetUserNotificationsQuery(string UserId) : IQuery<List<NotificationDto>>;
