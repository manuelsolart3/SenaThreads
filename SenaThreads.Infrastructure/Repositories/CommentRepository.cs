using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Infrastructure.Repositories;
public class CommentRepository : Repository<Comment>, ICommentRepository
{
    private readonly AppDbContext _appDbContext;

    public CommentRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<Comment>> GetCommentsByTweetIdAsync(Guid tweetId)
    {
        return await _appDbContext.Comments 
            .Where(c => c.TweetId == tweetId)// Filtrar los comentarios por el ID del tweet
            .Include(c => c.User) //Incluir la entidad user asociada a cada comentario
            .Include(c => c.Text) //Incluir el texto del comentario
            .ToListAsync(); 
    }
}
