using System.Data.Entity;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Notifications;

namespace SenaThreads.Infrastructure.Repositories;
public class NotificationRepository : Repository<Notification>, INotificationRepository
{
    private readonly AppDbContext _appDbContext;
    public NotificationRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<Notification>> GetUserNotificationsAsync(string userId)
    {
       return await _appDbContext.Notifications
           .Where(n => n.UserId == userId)
           .Include(n => n.User) //Incluir la info del usuario
           .ToListAsync();
    }
}
