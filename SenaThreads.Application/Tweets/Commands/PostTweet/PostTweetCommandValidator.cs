using FluentValidation;

namespace SenaThreads.Application.Tweets.Commands.PostTweet;
public class PostTweetValidator : AbstractValidator<PostTweetCommand>
{
    public PostTweetValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
        RuleFor(x => x.Text).NotEmpty().WithMessage("Text is required");
        RuleFor(x => x.Text).MaximumLength(300).WithMessage("Text cannot exceed 300 characters");
    }
}
