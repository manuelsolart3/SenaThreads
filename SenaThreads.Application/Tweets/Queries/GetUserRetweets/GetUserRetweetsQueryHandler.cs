using AutoMapper;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Tweets;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Tweets.Queries.GetUserRetweets;
public class GetUserRetweetsQueryHandler : IQueryHandler<GetUserRetweetsQuery, List<BasicTweetInfoDto>>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IMapper _mapper;
    public GetUserRetweetsQueryHandler(ITweetRepository tweetRepository, IMapper mapper)
    {
        _tweetRepository = tweetRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<BasicTweetInfoDto>>> Handle(GetUserRetweetsQuery request, CancellationToken cancellationToken)
    {
        //Obtener los retweets del usuario especifico usando la condicion en true
        List<Tweet> retweets = await _tweetRepository.GetAllTweetsAsync(request.UserId, retweetsOnly: true);

        List<BasicTweetInfoDto> retweetsDto = _mapper.Map<List<BasicTweetInfoDto>>(retweets);

        return Result.Success(retweetsDto);
    }
}
