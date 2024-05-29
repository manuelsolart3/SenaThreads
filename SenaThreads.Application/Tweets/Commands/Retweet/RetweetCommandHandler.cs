using MediatR;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Repositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;
using RetweetEntity = SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Tweets.Commands.Retweet;
public class RetweetCommandHandler : ICommandHandler<RetweetCommand>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RetweetCommandHandler(ITweetRepository tweetRepository, IUnitOfWork unitOfWork)
    {
        _tweetRepository = tweetRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result>Handle(RetweetCommand request, CancellationToken cancellationToken)
    { //Obtener el tweet al que se requiere Retweetear
        Tweet tweet = await _tweetRepository.GetByIdAsync(request.TweetId);
        //Verificamos que el tweet exista en la Bd  
        if (tweet == null)
        {
            return Result.Failure(Error.None);//No se encontro ningun Tweet
        }

        //Verificar si el usuario ya retwiteo este Tweet
        bool alreadyRetweed = tweet.Retweets.Any(x => x.RetweetedById == request.RetweetedById);
        if (alreadyRetweed)
        {
            return Result.Failure(Error.None); //Cambiar, deberia ser un error de redundancia o conflicto
        }
        //Crear el nuevo Retweet
        //tweet.Retweets.Add(new Retweet (request.TweetId, request.RetweetedById, request.comment));

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
