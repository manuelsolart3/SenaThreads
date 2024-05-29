using FluentValidation;

namespace SenaThreads.Application.Tweets.Commands.AddCommentToTweet;
public class AddCommentToTweetValidator : AbstractValidator<AddCommentToTweetCommand>
{
    public AddCommentToTweetValidator()
    {
        RuleFor(x => x.Text).NotEmpty().WithMessage("El texto del comentario es obligatorio");
    }
}
