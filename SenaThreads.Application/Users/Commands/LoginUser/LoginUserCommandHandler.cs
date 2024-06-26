using Microsoft.AspNetCore.Identity;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Authentication;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.Commands.LoginUser;
public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, object>
{
    private readonly UserManager<User> _userManager;
    private readonly JwtService _jwtService;

    public LoginUserCommandHandler(UserManager<User> userManager, JwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<Result<object>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        User user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return Result.Failure<object>(UserError.UserNotFound);
        }

         // Verificar contraseña del usuario
        var result = await _userManager.CheckPasswordAsync(user, request.Password);
        if (result)
        {
            var token = _jwtService.GenerateToken(user);
            return Result.Success<object>(new { Token = token });
        }
        else
        {
            return Result.Failure<object>(UserError.InvalidCredentials);
        }
    }
}
