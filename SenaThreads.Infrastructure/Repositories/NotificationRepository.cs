using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Notifications;

namespace SenaThreads.Infrastructure.Repositories;
public class NotificationRepository : Repository<Notification>, INotificationRepository
{
    public NotificationRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<List<Notification>> GetUserNotificationsAsync(string userId)
    {
       return await _dbSet
           .Where(n => n.UserId == userId)
           .Include(n => n.User) //Incluir la info del usuario
           .ToListAsync();
    }
}
