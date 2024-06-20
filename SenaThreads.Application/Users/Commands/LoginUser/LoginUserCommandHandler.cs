using Microsoft.AspNetCore.Identity;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Authentication;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.Commands.LoginUser;
public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, string>
{
    private readonly UserManager<User> _userManager;
    private readonly JwtService _jwtService;

    public LoginUserCommandHandler(UserManager<User> userManager, JwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        User user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return Result.Failure<string>(UserError.InvalidCredentials);
        }

        // Intento de inicio de sesión 
        var result = await _userManager.CheckPasswordAsync(user, request.Password);
        if (result)
        {
            var token = _jwtService.GenerateToken(user);
            return Result.Success(token);
        }
        else
        {
            return Result.Failure<string>(UserError.InvalidCredentials);
        }
    }
}
