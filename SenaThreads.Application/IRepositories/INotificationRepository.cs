using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Notifications;

namespace SenaThreads.Application.IRepositories;
public interface INotificationRepository : IRepository<Notification>
{
    Task<List<Notification>> GetUserNotificationsAsync(string userId);
}
