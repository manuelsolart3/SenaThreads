using Microsoft.AspNetCore.Identity;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.Commands.LoginUser;
public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand>
{
    // Inyección de dependencia del SignInManager para la gestión de inicio de sesión
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

  
    public LoginUserCommandHandler(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<Result> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        User user = await _userManager.FindByEmailAsync(request.Email);

        // Intento de inicio de sesión utilizando el SignInManager
        var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return Result.Success();
        }
        else
        {
            return Result.Failure(UserError.InvalidCredentials);
        }
    }
}
