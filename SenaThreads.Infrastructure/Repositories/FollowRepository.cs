using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Users;

namespace SenaThreads.Infrastructure.Repositories;

public class FollowRepository : Repository<Follow>, IFollowRepository
{
    public FollowRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<List<User>> GetFollowersInfoAsyn(string userId)
    {
        return await  _dbSet
            .Where(f => f.FollowedByUserId == userId)
            .Include(u => u.FollowerUser) //Incluir la info del usuario
            .Select(f => f.FollowerUser) //Solo los usuarios seguidores
            .ToListAsync();

    }

    public async Task<bool> IsFollowing(string followerUserId, string followedUserId)
    {
        // Consulta para verificar si ya el usuario followerUserId está siguiendo a followedUserId
        return await _dbSet
            .AnyAsync(f => f.FollowerUserId == followerUserId 
            && f.FollowedByUserId == followedUserId);
    }
}
