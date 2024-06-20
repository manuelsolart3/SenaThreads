using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Notifications;

namespace SenaThreads.Infrastructure.Repositories;
public class NotificationRepository : Repository<Notification>, INotificationRepository
{
    public NotificationRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
