using Microsoft.AspNetCore.Http;
using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Users.Commands.UploadProfilePicture;
public record UploadProfilePictureCommand(
    string UserId,
    IFormFile ProfilePictureS3key): ICommand;
