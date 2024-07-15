using FluentValidation;

namespace SenaThreads.Application.Tweets.Commands.AddCommentToTweet;
public class AddCommentToTweetValidator : AbstractValidator<AddCommentToTweetCommand>
{
    public AddCommentToTweetValidator()
    {
        RuleFor(x => x.tweetId).NotEmpty().WithMessage("TweetId is required.");
        RuleFor(x => x.userId).NotEmpty().WithMessage("UserId is required.");
        RuleFor(x => x.tweetId).NotEmpty().WithMessage("The text of the commentary is required");
    }
}
