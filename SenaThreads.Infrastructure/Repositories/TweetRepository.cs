using SenaThreads.Application.Repositories;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Infrastructure.Repositories;

public class TweetRepository : Repository<Tweet>, ITweetRepository
{
    public TweetRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
