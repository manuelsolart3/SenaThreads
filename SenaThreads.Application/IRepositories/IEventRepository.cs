using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Events;

namespace SenaThreads.Application.IRepositories;
public interface IEventRepository : IRepository<Event>
{
    Task<List<Event>> GetAllEventsAsync();
    Task<List<Event>> GetUserEventsAsync(string userId);
}
