using Microsoft.AspNetCore.Http;
using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Users.Commands.UploadProfilePicture;
public record UploadProfilePictureCommand(
    string userId,
    IFormFile profilePictureS3key): ICommand;
