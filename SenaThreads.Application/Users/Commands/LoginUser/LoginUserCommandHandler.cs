using Microsoft.AspNetCore.Identity;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.Commands.LoginUser;
public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand>
{
    private readonly UserManager<User> _userManager;

    public LoginUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        User user = await _userManager.FindByEmailAsync(request.Email);

        // Intento de inicio de sesión 
        var result = await _userManager.CheckPasswordAsync(user, request.Password);
        if (result)
        {
            return Result.Success();
        }
        else
        {
            return Result.Failure(UserError.InvalidCredentials);
        }
    }
}
