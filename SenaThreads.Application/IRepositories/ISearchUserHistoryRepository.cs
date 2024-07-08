using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.IRepositories;
public interface ISearchUserHistoryRepository : IRepository<SearchUserHistory>
{
    Task AddAsync(SearchUserHistory searchHistory);
}
