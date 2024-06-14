using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Users;

namespace SenaThreads.Infrastructure.Repositories;

public class UserBlockRepository : Repository<UserBlock>, IUserBlockRepository
{
    public UserBlockRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<bool> IsBlocked(string blockedUserId, string blockByUserId)
    {
        return await _dbSet
            .AnyAsync(b => b.BlockedUserId == blockedUserId 
            && b.BlockByUserId == blockByUserId);
    }
}
