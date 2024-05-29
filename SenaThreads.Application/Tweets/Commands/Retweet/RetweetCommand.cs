using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Tweets.Commands.Retweet;
public record RetweetCommand(
    Guid TweetId,
    string RetweetedById,
    string comment) : ICommand;
