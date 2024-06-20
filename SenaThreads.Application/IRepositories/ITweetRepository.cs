using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.IRepositories;

public interface ITweetRepository : IRepository<Tweet>
{
}
