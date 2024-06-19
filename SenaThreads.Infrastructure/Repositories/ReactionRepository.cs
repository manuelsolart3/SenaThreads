using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Infrastructure.Repositories;
public class ReactionRepository : Repository<Reaction>, IReactionRepository
{
    public ReactionRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
