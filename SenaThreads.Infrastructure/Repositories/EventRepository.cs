using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Events;

namespace SenaThreads.Infrastructure.Repositories;
public class EventRepository : Repository<Event>, IEventRepository
{
    public EventRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<Event> FindByIdAsync(Guid eventId)
    {
       return await _context.Events
            .Include(e => e.User)
            .FirstOrDefaultAsync(e => e.Id == eventId);
    }
}
