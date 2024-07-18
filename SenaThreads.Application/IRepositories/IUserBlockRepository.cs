using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.IRepositories;

public interface IUserBlockRepository : IRepository<UserBlock>
{
    Task<bool> IsBlocked(string blockedUserId, string blockByUserId);

    Task<ISet<string>> GetMutuallyBlockedUserIds(string userId);

}
