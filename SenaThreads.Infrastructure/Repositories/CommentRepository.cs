using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Infrastructure.Repositories;
public class CommentRepository : Repository<Comment>, ICommentRepository
{
    public CommentRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
