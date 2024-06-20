using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Tweets;
using SenaThreads.Domain.Users;

namespace SenaThreads.Infrastructure.Repositories;

public class TweetRepository : Repository<Tweet>, ITweetRepository
{
    public TweetRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
