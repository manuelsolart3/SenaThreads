
using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Users.Commands.UpdateProfile;
public record UpadateProfileCommand(
   string UserId,
    string PhoneNumber,
    string Biography,
    string City,
    DateOnly DateOfBirth) : ICommand;
