using Microsoft.AspNetCore.Identity;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.Commands.ValidateToken;
public class ValidateTokenCommandHandler : ICommandHandler<ValidateTokenCommand, TokenValidationResultDto>
{
    private readonly UserManager<User> _userManager;

    public ValidateTokenCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<TokenValidationResultDto>> Handle(ValidateTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.email);
        if (user is null)
        {
            return Result.Failure<TokenValidationResultDto>(UserError.UserNotFound);
        }

        var isValidToken = await _userManager.VerifyUserTokenAsync(user, "ShortLivedToken", "ResetPassword", request.token);

        if (!isValidToken)
        {
            return Result.Failure<TokenValidationResultDto>(UserError.InvalidToken);
        }

        // Crear el DTO con el email y el token
        var validationResult = new TokenValidationResultDto
        {
            Email = request.email,
            Token = request.token
        };

        // Devolver el DTO como parte del resultado
        return Result.Success(validationResult);
    }
}

