using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Tweets.Commands.RemoveReaction;
public record RemoveReactionCommand(Guid tweetId, string userId) : ICommand;
