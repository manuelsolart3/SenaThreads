using FluentValidation;

namespace SenaThreads.Application.Tweets.Commands.Retweet;
public class RetweetValidator : AbstractValidator<RetweetCommand>
{
    public RetweetValidator()
    {
        RuleFor(x => x.TweetId).NotEmpty().WithMessage("TweetId is required.");
        RuleFor(x => x.RetweetedById).NotEmpty().WithMessage("RetweetedById is required.");
    }
}
