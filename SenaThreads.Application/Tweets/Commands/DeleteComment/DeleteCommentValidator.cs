using FluentValidation;

namespace SenaThreads.Application.Tweets.Commands.DeleteComment;
public class DeleteCommentValidator : AbstractValidator<DeleteCommentCommand>
{
    public DeleteCommentValidator()
    {
        RuleFor(x => x.commentId).NotEmpty().WithMessage("CommentId is required.");
        RuleFor(x => x.tweetId).NotEmpty().WithMessage("TweetId is required");
        RuleFor(x => x.userId).NotEmpty().WithMessage("UserId is required.");
    }
}
