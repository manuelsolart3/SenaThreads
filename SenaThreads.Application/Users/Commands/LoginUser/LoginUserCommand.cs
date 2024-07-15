
using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Users.Commands.LoginUser;
public record LoginUserCommand(
    string email,
    string password) : ICommand<object>;
