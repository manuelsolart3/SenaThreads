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
    private readonly ILogger<RetweetCommandHandler> _logger;

    public RetweetCommandHandler(
        ITweetRepository tweetRepository,
        IRetweetRepository retweetRepository,
        IUnitOfWork unitOfWork,
        ILogger<RetweetCommandHandler> logger)
    {
        _tweetRepository = tweetRepository;
        _retweetRepository = retweetRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result> Handle(RetweetCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling RetweetCommand for TweetId: {TweetId} by UserId: {UserId}", request.TweetId, request.RetweetedById);

        // Obtener el tweet al que se requiere retweetear
        Tweet tweet = await FetchTweetByIdWithRetweets(request.TweetId);

        // Verificar si el tweet existe en la base de datos
        if (tweet == null)
        {
            _logger.LogWarning("Tweet not found: {TweetId}", request.TweetId);
            return Result.Failure(TweetError.NotFound);
        }

        // Verificar si el usuario ya retwitteó este tweet
        bool alreadyRetweeted = tweet.Retweets.Any(x => x.RetweetedById == request.RetweetedById);
        if (alreadyRetweeted)
        {
            _logger.LogWarning("User {UserId} already retweeted TweetId: {TweetId}", request.RetweetedById, request.TweetId);
            return Result.Failure(TweetError.AlreadyRetweeted);
        }

        var newRetweet = new RetweetEntity(
            request.TweetId,
            request.RetweetedById,
            request.Comment
        );

        // Agregar el nuevo retweet al repositorio de retweets
        _retweetRepository.Add(newRetweet);

        // Agregar el retweet a la colección de retweets del tweet 
        tweet.Retweets.Add(newRetweet);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Retweet successful for TweetId: {TweetId} by UserId: {UserId}", request.TweetId, request.RetweetedById);
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

