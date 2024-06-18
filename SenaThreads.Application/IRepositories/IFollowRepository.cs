using SenaThreads.Application.Dtos.Users;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.IRepositories;

public interface IFollowRepository : IRepository<Follow>
{
    Task<bool> IsFollowing(string followerUserId, string followedUserId);
}
