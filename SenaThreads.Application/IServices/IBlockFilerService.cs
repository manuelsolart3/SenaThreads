namespace SenaThreads.Application.IServices;
public interface IBlockFilterService
{
    Task<bool> ShouldFilterContent(string contentOwnerId, string viewerId);
    Task<IEnumerable<T>> FilterBlockedContent<T>(IEnumerable<T> content, string viewerId, Func<T, string> getOwnerId);
}
