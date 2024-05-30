using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Users;

namespace SenaThreads.Infrastructure.Repositories;

public class UserBlockRepository : Repository<UserBlock>, IUserBlockRepository
{
    public UserBlockRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
