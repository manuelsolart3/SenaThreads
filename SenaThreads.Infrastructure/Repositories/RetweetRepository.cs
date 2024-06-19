using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Infrastructure.Repositories;
public class RetweetRepository : Repository<Retweet>, IRetweetRepository
{
    public RetweetRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
