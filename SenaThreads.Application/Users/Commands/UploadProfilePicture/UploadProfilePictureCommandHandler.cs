using Microsoft.AspNet.Identity;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.Commands.UploadProfilePicture;
public class UploadProfilePictureCommandHandler : ICommandHandler<UploadProfilePictureCommand>
{
    private readonly UserManager<User> _userManager;

    public UploadProfilePictureCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(UploadProfilePictureCommand request, CancellationToken cancellationToken)
    {
      User user = await _userManager.FindByIdAsync(request.UserId);
        if (user is  null)
        { 
        return Result.Failure(UserError.UserNotFound);
        }

        //Actualizamos o subimos la imagen de perfil
        user.ProfilePictureS3Key = request.ProfilePictureS3Key;
        
        await _userManager.UpdateAsync(user);
        return Result.Success();
    }
}
