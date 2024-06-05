using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Infrastructure.Repositories;

public class TweetRepository : Repository<Tweet>, ITweetRepository
{
    private readonly AppDbContext _appDbContext;
    public TweetRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        
        _appDbContext = appDbContext;
    }
    public async Task<List<Tweet>> GetAllTweetsAsync()
    {
        return await _appDbContext.Tweets
          .Include(t => t.User) // Incluir el usuario que creó el Tweet
          .Include(t => t.Attachments) // Incluir los adjuntos
          .Include(t => t.Comments) // Incluir los comentarios
          .Include(t => t.Retweets) // Incluir los retweets
          .Include(t => t.Reactions) // Incluir las reacciones
          .ToListAsync();

    }

    public async Task<List<Tweet>> GetMediaTweetsByUserIdAsync(string userId)
    {
        return await _appDbContext.Tweets
        .Where(t => t.UserId == userId && t.Attachments.Any())
        .ToListAsync();
    }

    public async Task<List<Tweet>> GetTweetsByUserIdAsync(string userId)
    {
       return await _appDbContext.Tweets
            .Where(t => t.UserId == userId) 
            .ToListAsync();
    }
}
