
using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Users.Commands.LoginUser;
public record LoginUserCommand(
    string Email,
    string Password) : ICommand<string>;
