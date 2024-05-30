using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Users.Commands.RegisterUser;
public record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string UserName,
    string Password) :ICommand;
