﻿using Microsoft.AspNetCore.Identity;
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

        // Actualizar solo los campos que no sean nulos
        if (request.PhoneNumber is not null)
        {
            user.PhoneNumber = request.PhoneNumber;
        }

        if (request.Biography is not null)
        {
            user.Biography = request.Biography;
        }

        if (request.City is not null)
        {
            user.City = request.City;
        }

        if (request.DateOfBirth.HasValue)
        {
            user.DateOfBirth = request.DateOfBirth.Value;
        }


        //Guardar los cambios
        var updateResult = await _userManager.UpdateAsync(user);

        if (updateResult.Succeeded)
        {
            return Result.Success();
        }
        else
        {
            return Result.Failure(UserError.ErrorupdateResult);
        }
    }
}
