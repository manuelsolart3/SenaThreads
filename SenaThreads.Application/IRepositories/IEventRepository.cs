using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Events;

namespace SenaThreads.Application.IRepositories;
public interface IEventRepository : IRepository<Event>
{
}
