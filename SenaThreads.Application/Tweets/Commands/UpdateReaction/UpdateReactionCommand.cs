using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Tweets.Commands.UpdateReaction;
public record UpdateReactionCommand
    (string userId,
    Guid tweetId,
    ReactionType newReactionType) : ICommand;
