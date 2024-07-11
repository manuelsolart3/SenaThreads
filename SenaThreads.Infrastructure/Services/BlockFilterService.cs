using SenaThreads.Application.IRepositories;
using SenaThreads.Application.IServices;

namespace SenaThreads.Infrastructure.Services;
public class BlockFilterService : IBlockFilterService
{
    private readonly IUserBlockRepository _userBlockRepository;

    public BlockFilterService(IUserBlockRepository userBlockRepository)
    {
        _userBlockRepository = userBlockRepository;
    }

    public async Task<bool> ShouldFilterContent(string contentOwnerId, string viewerId)
    {
        return await _userBlockRepository.IsBlocked(contentOwnerId, viewerId);
    }

    public async Task<IEnumerable<T>> FilterBlockedContent<T>(IEnumerable<T> content, string currentUserId, Func<T, string> getOwnerId)
    {
        var mutuallyBlockedUserIds = await _userBlockRepository.GetMutuallyBlockedUserIds(currentUserId);
        return content.Where(item => !mutuallyBlockedUserIds.Contains(getOwnerId(item)));
    }
}
