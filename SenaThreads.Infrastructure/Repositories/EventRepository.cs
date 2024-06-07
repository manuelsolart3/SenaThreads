using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Events;

namespace SenaThreads.Infrastructure.Repositories;
public class EventRepository : Repository<Event>, IEventRepository
{
    private readonly AppDbContext _appDbContext;
    public EventRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<Event>> GetAllEventsAsync()
    {
        return await _appDbContext.Events
            .Include(e => e.User) //Informacion del usuario creador
            .ToListAsync();
    }

    public async Task<List<Event>> GetUserEventsAsync(string userId)
    {
        return await _appDbContext.Events
            .Where(e => e.UserId == userId)
            .Include(e => e.User)//Informacion del usuario creador
            .ToListAsync();
    }
}
