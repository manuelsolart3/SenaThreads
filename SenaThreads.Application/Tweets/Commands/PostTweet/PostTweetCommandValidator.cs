using FluentValidation;

namespace SenaThreads.Application.Tweets.Commands.PostTweet;
public class PostTweetValidator : AbstractValidator<PostTweetCommand>
{
    public PostTweetValidator()
    {
        RuleFor(x => x.userId).NotEmpty().WithMessage("UserId is required.");
        RuleFor(x => x.text).NotEmpty().WithMessage("Text is required");
        RuleFor(x => x.text).MaximumLength(300).WithMessage("Text cannot exceed 300 characters");
    }
}
