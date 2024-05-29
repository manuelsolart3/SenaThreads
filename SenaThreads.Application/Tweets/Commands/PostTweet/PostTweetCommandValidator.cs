using FluentValidation;

namespace SenaThreads.Application.Tweets.Commands.PostTweet;
public class PostTweetCommandValidator : AbstractValidator<PostTweetCommand>
{
    public PostTweetCommandValidator()
    {
        RuleFor(x => x.Text).NotEmpty().WithMessage("El texto del tweet es obligatorio.");
        RuleFor(x => x.Text).MaximumLength(300).WithMessage("El texto del tweet no puede tener más de 300 caracteres.");
    }
}
