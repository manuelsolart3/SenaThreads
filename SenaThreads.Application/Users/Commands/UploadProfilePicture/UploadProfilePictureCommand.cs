using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Users.Commands.UploadProfilePicture;
public record UploadProfilePictureCommand(
    string UserId,
    string ProfilePictureS3Key): ICommand;
