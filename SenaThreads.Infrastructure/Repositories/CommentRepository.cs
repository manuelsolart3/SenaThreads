using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Infrastructure.Repositories;
public class CommentRepository : Repository<Comment>, ICommentRepository
{
    public CommentRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<List<Comment>> GetCommentsByTweetIdAsync(Guid tweetId)
    {
        return await _dbSet
            .Where(c => c.TweetId == tweetId)// Filtrar los comentarios por el ID del tweet
            .Include(c => c.User) //Incluir la entidad user asociada a cada comentario
            .Include(c => c.Text) //Incluir el texto del comentario
            .ToListAsync(); 
    }
}
