using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Tweets.Commands.AddCommentToTweet;
public record AddCommentToTweetCommand(
    Guid TweetId,
    string UserId,
    string Text) : ICommand;
