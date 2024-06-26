using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SenaThreads.Application.IServices;

namespace SenaThreads.Infrastructure.Services;
public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }


    public Guid UserId =>
      Guid.Parse(
          _httpContextAccessor
          .HttpContext?
          .User
          .FindFirstValue(ClaimTypes.NameIdentifier) ??
          throw new ApplicationException("User Context is unavailable"));


    public bool IsAuthenticated =>
        _httpContextAccessor.
        HttpContext?
        .User
        .Identity?
        .IsAuthenticated ??
        throw new ApplicationException("User Context is unavailable");
}
