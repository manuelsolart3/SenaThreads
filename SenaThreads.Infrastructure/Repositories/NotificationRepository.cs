using SenaThreads.Application.Repositories;
using SenaThreads.Domain.Notifications;

namespace SenaThreads.Infrastructure.Repositories;
public class NotificationRepository : Repository<Notification>, INotificationRepository
{
    public NotificationRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
