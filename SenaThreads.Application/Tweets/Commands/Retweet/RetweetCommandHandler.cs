using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;
using RetweetEntity = SenaThreads.Domain.Tweets.Retweet;

namespace SenaThreads.Application.Tweets.Commands.Retweet;
public class RetweetCommandHandler : ICommandHandler<RetweetCommand>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IRetweetRepository _retweetRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RetweetCommandHandler(
        ITweetRepository tweetRepository,
        IRetweetRepository retweetRepository,
        IUnitOfWork unitOfWork
       )
    {
        _tweetRepository = tweetRepository;
        _retweetRepository = retweetRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RetweetCommand request, CancellationToken cancellationToken)
    {


        // Obtener el tweet al que se requiere retweetear
        Tweet tweet = await FetchTweetByIdWithRetweets(request.tweetId);

        // Verificar si el tweet existe en la base de datos
        if (tweet is null)
        {

            return Result.Failure(TweetError.NotFound);
        }

        // Verificar si el usuario ya retwitteó este tweet
        bool alreadyRetweeted = tweet.Retweets.Any(x => x.RetweetedById == request.retweetedById);
        if (alreadyRetweeted)
        {

            return Result.Failure(TweetError.AlreadyRetweeted);
        }

        var newRetweet = new RetweetEntity(
            request.tweetId,
            request.retweetedById,
            request.comment
        );

        // Agregar el nuevo retweet al repositorio de retweets
        _retweetRepository.Add(newRetweet);

        // Agregar el retweet a la colección de retweets del tweet 
        tweet.Retweets.Add(newRetweet);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    private async Task<Tweet> FetchTweetByIdWithRetweets(Guid tweetId)
    {
        return await _tweetRepository
            .Queryable()
            .Where(tweet => tweet.Id == tweetId)
            .Include(tweet => tweet.Retweets)
            .FirstOrDefaultAsync();
    }

}

