using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Tweets.Commands.Retweet;
public record RetweetCommand(
    Guid TweetId,
    string RetweetedById,
    string Comment) : ICommand;
