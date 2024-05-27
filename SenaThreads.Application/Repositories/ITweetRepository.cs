using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Repositories;

public interface ITweetRepository : IRepository<Tweet>
{
}
