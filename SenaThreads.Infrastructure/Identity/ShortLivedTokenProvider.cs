using Microsoft.AspNetCore.Identity;

namespace SenaThreads.Web;

public class ShortLivedTokenProvider<TUser> : IUserTwoFactorTokenProvider<TUser> where TUser : class
{
    public async Task<string> GenerateAsync(string purpose, UserManager<TUser> manager, TUser user)
    {
        return Guid.NewGuid().ToString("N").Substring(0, 8);
    }

    public async Task<bool> ValidateAsync(string purpose, string token, UserManager<TUser> manager, TUser user)
    {
        return true;
    }

    public async Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<TUser> manager, TUser user)
    {
        return true;
    }
}
