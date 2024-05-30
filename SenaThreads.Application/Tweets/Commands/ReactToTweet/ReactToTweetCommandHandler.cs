using System.Data.Entity;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Tweets.Commands.ReactToTweet;
public class ReactToTweetCommandHandler : ICommandHandler<ReactToTweetCommand>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReactToTweetCommandHandler(
        ITweetRepository tweetRepository,
        IUnitOfWork unitOfWork)
    {
        _tweetRepository = tweetRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ReactToTweetCommand request, CancellationToken cancellationToken)
    {

        //Obtener el tweet al que se requiere Reaccionar
        Tweet tweet = await FetchTweetByIdWithReactions(request.TweetId);
        // Verificar si el Tweet existe en la BD
        if (tweet == null)
        {
            return Result.Failure(TweetError.NotFound);//no se encontro ningun tweet
        }

        // Verificar si el usuario ya reaccionó a este Tweet
        Reaction existingReaction = tweet.Reactions.FirstOrDefault(x => x.UserId == request.UserId);
        if (existingReaction != null)
        {

            if (existingReaction.Type != request.Type) //comparamos si el tipo es el mismo
            {
                existingReaction.Type = request.Type; // Actualizar la reacción si es diferente
                return Result.Success(); //Devolver un resultado para indicar que se actualizo
            }
        }
        else
        {
            //lo agregamos a la  coleccion de reacciones en el Tweet
            tweet.Reactions.Add(new Reaction( // Agregamos una nueva instancia de reaction con los parametros del command 
                request.TweetId,
                request.UserId,
                request.Type));
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    private async Task<Tweet> FetchTweetByIdWithReactions(Guid TweetId)
    {
        return await _tweetRepository
            .Queryable()
            .Where(tweet => tweet.Id == TweetId)
            .Include(tweet => tweet.Reactions)
            .FirstOrDefaultAsync();
    }
}
