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
        // Verificar si el correo electrónico ya existe
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
        {
            return Result.Failure(UserError.EmailAlreadyExists);
        } 

        // Verificar si el nombre de usuario ya existe
        existingUser = await _userManager.FindByNameAsync(request.UserName);
        if (existingUser != null)
        {
            return Result.Failure(UserError.UsernameAlreadyExists);
        }



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
