using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Users.Commands.BlockUser;
public record BlockUserCommand(
    string blockedUserId,
    string blockByUserId) :ICommand;
