using System.Data.Entity;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Tweets.Queries.GetUserTweets;
public class GetUserTweetsQueryHandler : IQueryHandler<GetUserTweetsQuery, List<Tweet>>
{
    private readonly ITweetRepository _tweetRepository;

    public GetUserTweetsQueryHandler(ITweetRepository tweetRepository)
    {
        _tweetRepository = tweetRepository;
    }

    public async Task<Result<List<Tweet>>> Handle(GetUserTweetsQuery request, CancellationToken cancellationToken)
    {
        List<Tweet> userTweets = await _tweetRepository
            .Queryable()
            .Where(tweet => tweet.UserId == request.UserId)
            .ToListAsync(cancellationToken);

        return Result.Success(userTweets);
    }
}
