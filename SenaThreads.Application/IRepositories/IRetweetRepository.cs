using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.IRepositories;
public interface IRetweetRepository : IRepository<Retweet>
{
}
