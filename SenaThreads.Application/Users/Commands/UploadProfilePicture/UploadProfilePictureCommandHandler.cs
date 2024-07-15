using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Identity;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.ExternalServices;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.Commands.UploadProfilePicture;
public class UploadProfilePictureCommandHandler : ICommandHandler<UploadProfilePictureCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IAwsS3Service _awsS3Service;

    public UploadProfilePictureCommandHandler(UserManager<User> userManager, IAwsS3Service awsS3Service)
    {
        _userManager = userManager;
        _awsS3Service = awsS3Service;
    }

    public async Task<Result> Handle(UploadProfilePictureCommand request, CancellationToken cancellationToken)
    {
        User user = await _userManager.FindByIdAsync(request.UserId);

        if (user is  null)
        { 
        return Result.Failure(UserError.UserNotFound);
        }

        // Subir la imagen de perfil a AWS S3
        string profilePicturekey = await _awsS3Service.UploadFileToS3Async(request.ProfilePictureS3key);

        //Actualizar o subir la imagen de perfil
        user.ProfilePictureS3Key = profilePicturekey;
        
        //Guardar cambios en el usuario
        var updatedResult = await _userManager.UpdateAsync(user);

        if (updatedResult.Succeeded)
        {
            return Result.Success();
        }
        else
        {
            return Result.Failure(UserError.ErrorupdateResult);
        }
    }
}
