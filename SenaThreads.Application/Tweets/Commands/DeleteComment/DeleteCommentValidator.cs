using FluentValidation;

namespace SenaThreads.Application.Tweets.Commands.DeleteComment;
public class DeleteCommentValidator : AbstractValidator<DeleteCommentCommand>
{
    public DeleteCommentValidator()
    {
        RuleFor(x => x.CommentId).NotEmpty().WithMessage("CommentId is required.");
        RuleFor(x => x.TweetId).NotEmpty().WithMessage("TweetId is required");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
    }
}
