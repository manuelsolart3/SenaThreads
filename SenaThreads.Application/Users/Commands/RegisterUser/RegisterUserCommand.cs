using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Users.Commands.RegisterUser;
public record RegisterUserCommand(
    string firstName,
    string lastName,
    string email,
    string userName,
    string password) :ICommand;
