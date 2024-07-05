using Microsoft.AspNetCore.Identity;

namespace SenaThreads.Web;

public class ShortLivedTokenProvider<TUser> : IUserTwoFactorTokenProvider<TUser> where TUser : class
{
    private const int TokenLength = 8;
    private const int TokenExpirationMinutes = 60; 

    public async Task<string> GenerateAsync(string purpose, UserManager<TUser> manager, TUser user)
    {
        var token = Guid.NewGuid().ToString("N").Substring(0, TokenLength);
        var expirationTime = DateTime.UtcNow.AddMinutes(TokenExpirationMinutes);

        // Almacenar el token y su tiempo de expiración
        await manager.SetAuthenticationTokenAsync(user, "ShortLivedToken", purpose, $"{token}:{expirationTime.Ticks}");

        return token;
    }

    public async Task<bool> ValidateAsync(string purpose, string token, UserManager<TUser> manager, TUser user)
    {
        var storedToken = await manager.GetAuthenticationTokenAsync(user, "ShortLivedToken", purpose);

        if (string.IsNullOrEmpty(storedToken))
        {
            return false;
        }

        var parts = storedToken.Split(':');
        if (parts.Length != 2)
        {
            return false;
        }

        var storedTokenValue = parts[0];
        var expirationTicks = long.Parse(parts[1]);
        var expirationTime = new DateTime(expirationTicks, DateTimeKind.Utc);

        if (DateTime.UtcNow > expirationTime)
        {
            // El token ha expirado
            return false;
        }

        // Comparar el token almacenado con el token proporcionado
        return token == storedTokenValue;
    }

    public async Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<TUser> manager, TUser user)
    {
        return true;
    }
}
