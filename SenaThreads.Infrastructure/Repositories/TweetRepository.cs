using System.ComponentModel.Design;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Infrastructure.Repositories;

public class TweetRepository : Repository<Tweet>, ITweetRepository
{
    public TweetRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
