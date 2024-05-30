using System.Data.Entity;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Tweets.Queries.GetTweetWithImage;
public class GetTweetWithImageQueryHandler : IQueryHandler<GetTweetWithImageQuery, List<Tweet>>
{
    private readonly ITweetRepository _tweetRepository;

    public GetTweetWithImageQueryHandler(ITweetRepository tweetRepository)
    {
        _tweetRepository = tweetRepository;
    }

    public async Task<Result<List<Tweet>>> Handle(GetTweetWithImageQuery request, CancellationToken cancellationToken)
    {
        List<Tweet> tweetsWithImage = await _tweetRepository
             .Queryable()
             .Where(tweet => tweet.UserId == request.UserId 
             && tweet.Attachments.Any())
             .ToListAsync();

        return Result.Success(tweetsWithImage);
    }
}
