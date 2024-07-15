using MediatR;
using Microsoft.AspNetCore.Identity;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.Commands.ResetPassword;
public class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand>
{
    private readonly UserManager<User> _userManager;

    public ResetPasswordCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.email);
        if (user is null)
        {
            return Result.Failure(UserError.UserNotFound);
        }

        // Validación explícita del token
        var isValidToken = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", request.token);
        if (!isValidToken)
        {
            return Result.Failure(UserError.InvalidToken);
        }

        //Se resetea la contraseña del user encontrado
        var resetResult = await _userManager.ResetPasswordAsync(user, request.token, request.newPassword);
        if (!resetResult.Succeeded)
        {
            return Result.Failure(UserError.PasswordResetFailed);
        }

        return Result.Success();
    }
}
