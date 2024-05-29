using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Tweets.Commands.DeleteComment;
public record DeleteCommentCommand(
    Guid TweetId,
    Guid CommentId,
    string UserId) : ICommand;
