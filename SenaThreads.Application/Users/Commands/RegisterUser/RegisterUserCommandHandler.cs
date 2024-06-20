using Microsoft.AspNetCore.Identity;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Authentication;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.Commands.RegisterUser;
public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly UserManager<User> _userManager;//User Manager para la gestion de Users
   

    public RegisterUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
   
    }

    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        //Agregar una nueva instancia de user
        User newuser = new(
            request.FirstName,
            request.LastName,
            request.Email,
            request.UserName);

        //Llamar al metodo CreateAsync del UserManager para crear el U en la Bd
       var result = await _userManager.CreateAsync(newuser, request.Password);

        if (!result.Succeeded)
        {
            return Result.Failure(UserError.RegistrationFailed);
        }
        return Result.Success();
    }
}
