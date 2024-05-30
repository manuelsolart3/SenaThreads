using Microsoft.AspNetCore.Identity;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.Commands.UpdateProfile;
public class UpadateProfileCommandHandler : ICommandHandler<UpadateProfileCommand>
{
    private readonly UserManager<User> _userManager;

    public UpadateProfileCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(UpadateProfileCommand request, CancellationToken cancellationToken)
    {
        User user = await _userManager.FindByIdAsync(request.UserId);
        if (user is null)
        {
            return Result.Failure(UserError.UserNotFound);
        }

        //Crear o Actualizar propiedades opcionales
        user.PhoneNumber = request.PhoneNumber;
        user.Biography = request.Biography;
        user.City = request.City;
        user.DateOfBirth = request.DateOfBirth;

        //Guardar los cambios
        await _userManager.UpdateAsync(user);

        return Result.Success();

    }
}
