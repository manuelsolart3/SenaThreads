using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Tweets.Commands.DeleteTweet;
public record DeleteTweetCommand (
    Guid tweetId,
    string userId) : ICommand;

