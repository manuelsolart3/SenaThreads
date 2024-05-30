using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Users.Commands.UnBlockUser;
public record UnBlockUserCommand(
    string BlockedUserId,
    string BlockByUserId) : ICommand;
