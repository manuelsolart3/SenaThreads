using FluentValidation;

namespace SenaThreads.Application.Tweets.Commands.DeleteTweet;
public class DeleteTweetValidator : AbstractValidator<DeleteTweetCommand>
{
    public DeleteTweetValidator()
    {
        RuleFor(x => x.tweetId).NotEmpty().WithMessage("TweetId is required.");

        RuleFor(x => x.userId).NotEmpty().WithMessage("UserId is required.");
    }
}
