using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Users.Commands.FollowUser;
public record FollowUserCommand(
    string followerUserId,
    string followedByUserId) :ICommand;
