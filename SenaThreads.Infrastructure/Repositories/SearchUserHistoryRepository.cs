using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Users;

namespace SenaThreads.Infrastructure.Repositories;
public class SearchUserHistoryRepository : Repository<SearchUserHistory>, ISearchUserHistoryRepository
{
    public SearchUserHistoryRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<List<SearchUserHistory>> GetUserSearchHistoryAsync(string userId, int page, int pageSize)
    {
        return await _context.Set<SearchUserHistory>()
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.SearchedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}

