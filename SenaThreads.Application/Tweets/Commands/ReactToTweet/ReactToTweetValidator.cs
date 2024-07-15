using FluentValidation;
using SenaThreads.Application.Tweets.Commands.ReactToTweet;

namespace SenaThreads.Application.Tweets.Commands.PostTweet;
public class ReactToTweetValidator : AbstractValidator<ReactToTweetCommand>
{
    public ReactToTweetValidator()
    {
        RuleFor(x => x.tweetId).NotEmpty().WithMessage("TweetId is required.");
        RuleFor(x => x.userId).NotEmpty().WithMessage("UserId is required.");
        RuleFor(x => x.type).IsInEnum().WithMessage("Invalid reaction type");
    }
}
