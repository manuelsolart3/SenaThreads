namespace SenaThreads.Application.IServices;
public  interface IUserContextService
{
    Guid UserId { get; }
    bool IsAuthenticated { get; }
}
