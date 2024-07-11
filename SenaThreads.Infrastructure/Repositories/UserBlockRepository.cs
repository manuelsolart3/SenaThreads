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

    public async Task<ISet<string>> GetMutuallyBlockedUserIds(string userId)
    {
        var blockedUsers = await _context.UserBlocks
            .Where(ub => ub.BlockByUserId == userId || ub.BlockedUserId == userId)
            .Select(ub => ub.BlockByUserId == userId ? ub.BlockedUserId : ub.BlockByUserId)
            .ToListAsync();

        return new HashSet<string>(blockedUsers);
    }
}
