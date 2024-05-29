using FluentValidation;
using SenaThreads.Application.Tweets.Commands.ReactToTweet;

namespace SenaThreads.Application.Tweets.Commands.PostTweet;
public class ReactToTweetValidator : AbstractValidator<ReactToTweetCommand>
{
    public ReactToTweetValidator()
    {

        RuleFor(x => x.Type).IsInEnum().WithMessage("El tipo de reaccion no es valido");
    }
}
