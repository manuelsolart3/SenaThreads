using System.Data.Entity;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Users;

namespace SenaThreads.Infrastructure.Repositories;

public class FollowRepository : Repository<Follow>, IFollowRepository
{
    private readonly AppDbContext _appDbContext;
    public FollowRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<User>> GetFollowersInfoAsyn(string userId)
    {
        return await  _appDbContext.Follows
            .Where(f => f.FollowedByUserId == userId)
            .Include(u => u.FollowerUser) //Incluir la info del usuario
            .Select(f => f.FollowerUser) //Solo los usuarios seguidores
            .ToListAsync();

    }
}
