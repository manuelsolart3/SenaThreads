using System.Security.Cryptography;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.IServices;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.Commands.ForgotPassword;
public class ForgotPasswordCommandHandler : ICommandHandler<ForgotPasswordCommand>
{
     private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;

    public ForgotPasswordCommandHandler(UserManager<User> userManager, IEmailService emailService)
    {
        _userManager = userManager;
        _emailService = emailService;
    }

    public async Task<Result> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        // Buscar el usuario por su email
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null )
        {
            // Manejar el caso en que el usuario no existe o su email no está confirmado
            return Result.Failure(UserError.UserNotFound);
        }

        // Generar y guardar un token para restablecer la contraseña
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);


        // Enviar el token por correo electrónico al usuario
        await _emailService.SendPasswordResetEmail(user.Email, token);

        return Result.Success();
    }
}
