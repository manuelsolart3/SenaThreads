using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Users.Commands.UnFollowUser;
public record UnfollowUserCommand(
      string FollowerUserId,
    string FollowedByUserId) : ICommand;
