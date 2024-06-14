using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Events;

namespace SenaThreads.Infrastructure.Repositories;
public class EventRepository : Repository<Event>, IEventRepository
{
    public EventRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<List<Event>> GetAllEventsAsync()
    {
        return await _dbSet
            .Include(e => e.User) //Informacion del usuario creador
            .ToListAsync();
    }

    public async Task<List<Event>> GetUserEventsAsync(string userId)
    {
        return await _dbSet
            .Where(e => e.UserId == userId)
            .Include(e => e.User)//Informacion del usuario creador
            .ToListAsync();
    }
}
