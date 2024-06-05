using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.IRepositories;

public interface ITweetRepository : IRepository<Tweet>
{
    Task<List<Tweet>> GetAllTweetsAsync();
    Task<List<Tweet>> GetTweetsByUserIdAsync(string userId);
    Task<List<Tweet>> GetMediaTweetsByUserIdAsync(string userId);
}
