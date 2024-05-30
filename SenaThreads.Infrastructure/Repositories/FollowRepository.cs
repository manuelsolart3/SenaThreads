using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Users;

namespace SenaThreads.Infrastructure.Repositories;

public class FollowRepository : Repository<Follow>, IFollowRepository
{
    public FollowRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
