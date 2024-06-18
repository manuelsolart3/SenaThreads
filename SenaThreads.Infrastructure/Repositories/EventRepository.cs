using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Events;

namespace SenaThreads.Infrastructure.Repositories;
public class EventRepository : Repository<Event>, IEventRepository
{
    public EventRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
