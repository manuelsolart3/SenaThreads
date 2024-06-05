using AutoMapper;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Tweets;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Tweets.Queries.GetUserTweets;
public class GetUserTweetsQueryHandler : IQueryHandler<GetUserTweetsQuery, List<BasicTweetInfoDto>>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IMapper _mapper;

    public GetUserTweetsQueryHandler(ITweetRepository tweetRepository, IMapper mapper)
    {
        _tweetRepository = tweetRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<BasicTweetInfoDto>>> Handle(GetUserTweetsQuery request, CancellationToken cancellationToken)
    {
        List<Tweet> userTweets = await _tweetRepository.GetTweetsByUserIdAsync(request.UserId);
        List<BasicTweetInfoDto> tweetDtos = _mapper.Map<List<BasicTweetInfoDto>>(userTweets);
        return Result.Success(tweetDtos);
    }
}
