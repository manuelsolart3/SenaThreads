using FluentValidation;

namespace SenaThreads.Application.Tweets.Commands.AddCommentToTweet;
public class AddCommentToTweetValidator : AbstractValidator<AddCommentToTweetCommand>
{
    public AddCommentToTweetValidator()
    {
        RuleFor(x => x.TweetId).NotEmpty().WithMessage("TweetId is required.");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
        RuleFor(x => x.Text).NotEmpty().WithMessage("The text of the commentary is required");
    }
}
