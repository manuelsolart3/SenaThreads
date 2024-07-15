using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Tweets.Commands.Retweet;
public record RetweetCommand(
    Guid tweetId,
    string retweetedById,
    string comment) : ICommand;
