using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Notifications;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Notifications.QueriesN.GetUserNotifications;
public record GetUserNotificationsQuery(string UserId, int Page,int PageSize) : IQuery<Pageable<NotificationDto>>;
