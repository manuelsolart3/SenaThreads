using SenaThreads.Application.Abstractions.Messaging;

namespace SenaThreads.Application.Tweets.Commands.DeleteComment;
public record DeleteCommentCommand(
    Guid TweetId,
    Guid CommentId,
    string UserId) : ICommand;
