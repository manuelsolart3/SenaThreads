using FluentValidation;
using SenaThreads.Application.Tweets.Commands.ReactToTweet;

namespace SenaThreads.Application.Tweets.Commands.PostTweet;
public class ReactToTweetValidator : AbstractValidator<ReactToTweetCommand>
{
    public ReactToTweetValidator()
    {
        RuleFor(x => x.TweetId).NotEmpty().WithMessage("TweetId is required.");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
        RuleFor(x => x.Type).IsInEnum().WithMessage("Invalid reaction type");
    }
}
