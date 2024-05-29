using ICommand = SenaThreads.Application.Abstractions.Messaging.ICommand;

namespace SenaThreads.Application.Tweets.Commands.AddCommentToTweet;
public record AddCommentToTweetCommand(
    Guid TweetId,
    string UserId,
    string Text) : ICommand;
