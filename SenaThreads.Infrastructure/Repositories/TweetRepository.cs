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
    public async Task<List<Tweet>> GetAllTweetsAsync(string userId = null, bool retweetsOnly = false)
    {
        IQueryable<Tweet> query = _context.Tweets// Crear una consulta IQueryable de tweets
            .Include(t => t.User) // Incluir información del usuario creador del tweet
            .Include(t => t.Attachments) // Incluir adjuntos del tweet
            .Include(t => t.Reactions) // Incluir reacciones del tweet
            .Include(t => t.Comments); // Incluir comentarios del tweet

        if (!string.IsNullOrEmpty(userId) && retweetsOnly)
        {
            //Filtrar solo los Retweets que coincidan con el userId
            query = query.Where(t => t.Retweets.Any(r => r.RetweetedById == userId));
        }

        return await query.ToListAsync();
    }

    public async Task<List<Tweet>> GetMediaTweetsByUserIdAsync(string userId)
    {
        return await _dbSet
            .Include(t => t.User) // Incluir información del usuario creador del tweet
            .Include(t => t.Attachments) // Incluir adjuntos del tweet
            .Include(t => t.Reactions) // Incluir reacciones del tweet
            .Include(t => t.Retweets) // Incluir retweets del tweet
            .Include(t => t.Comments) // Incluir comentarios del tweet
        .Where(t => t.UserId == userId && t.Attachments.Any())
        .ToListAsync();
    }
}
