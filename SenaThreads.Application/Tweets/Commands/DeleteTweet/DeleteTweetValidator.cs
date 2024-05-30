using FluentValidation;

namespace SenaThreads.Application.Tweets.Commands.DeleteTweet;
public class DeleteTweetValidator : AbstractValidator<DeleteTweetCommand>
{
    public DeleteTweetValidator()
    {
        RuleFor(x => x.TweetId).NotEmpty().WithMessage("TweetId is required.");

        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
    }
}
