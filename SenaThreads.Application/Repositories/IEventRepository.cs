using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Events;

namespace SenaThreads.Application.Repositories;
public interface IEventRepository : IRepository<Event>
{
}
