using FluentValidation;

namespace SenaThreads.Application.Tweets.Commands.Retweet;
public class RetweetValidator : AbstractValidator<RetweetCommand>
{
    public RetweetValidator()
    {
        RuleFor(x => x.tweetId).NotEmpty().WithMessage("TweetId is required.");
        RuleFor(x => x.retweetedById).NotEmpty().WithMessage("RetweetedById is required.");
    }
}
