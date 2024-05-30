using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Users.Commands.FollowUser;
public record FollowUserCommand(
    string FollowerUserId,
    string FollowedByUserId) :ICommand;
