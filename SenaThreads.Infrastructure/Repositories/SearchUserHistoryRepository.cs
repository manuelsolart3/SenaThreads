using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Users;

namespace SenaThreads.Infrastructure.Repositories;
public class SearchUserHistoryRepository : Repository<SearchUserHistory>, ISearchUserHistoryRepository
{
    public SearchUserHistoryRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task AddAsync(SearchUserHistory searchHistory)
    {
        await _context.SearchUserHistories.AddAsync(searchHistory);
    }
}

