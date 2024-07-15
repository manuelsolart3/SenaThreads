using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Tweets.Commands.AddCommentToTweet;
public record AddCommentToTweetCommand(
    Guid tweetId,
    string userId,
    string text) : ICommand;
