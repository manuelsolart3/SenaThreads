using AutoMapper;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Tweets;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Tweets.Queries.GetUserMediaTweets;
public class GetUserMediaTweetsQueryHandler : IQueryHandler<GetUserMediaTweetsQuery, List<BasicTweetInfoDto>>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IMapper _mapper;

    public GetUserMediaTweetsQueryHandler(ITweetRepository tweetRepository, IMapper mapper)
    {
        _tweetRepository = tweetRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<BasicTweetInfoDto>>> Handle(GetUserMediaTweetsQuery request, CancellationToken cancellationToken)
    {
        List<Tweet> mediaTweets = await _tweetRepository.GetMediaTweetsByUserIdAsync(request.UserId);
        List<BasicTweetInfoDto> tweetDtos = _mapper.Map<List<BasicTweetInfoDto>>(mediaTweets);
        return Result.Success(tweetDtos);
    }
}
