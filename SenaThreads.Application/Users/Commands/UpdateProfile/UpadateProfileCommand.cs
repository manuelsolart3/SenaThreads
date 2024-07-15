
using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Users.Commands.UpdateProfile;
public record UpadateProfileCommand(
   string userId,
    string? phoneNumber,
    string? biography,
    string? city,
    DateOnly? dateOfBirth) : ICommand;
