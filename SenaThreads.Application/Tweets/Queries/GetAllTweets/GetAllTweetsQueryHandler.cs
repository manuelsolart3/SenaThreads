using AutoMapper;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Tweets;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Tweets.Queries.GetAllTweets;
public class GetAllTweetsQueryHandler : IQueryHandler<GetAllTweetsQuery, List<BasicTweetInfoDto>>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IMapper _mapper;

    public GetAllTweetsQueryHandler(ITweetRepository tweetRepository, IMapper mapper)
    {
        _tweetRepository = tweetRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<BasicTweetInfoDto>>> Handle(GetAllTweetsQuery request, CancellationToken cancellationToken)
    {
        //Obtener todos los Tweets de la Bd
        List<Tweet> tweets = await _tweetRepository.GetAllTweetsAsync();

        //Mapear la lista de tweets a una list de Dto usando automap
        List<BasicTweetInfoDto> tweetsDto = _mapper.Map<List<BasicTweetInfoDto>>(tweets);

        //retornar la lista de Dtos de tweets
        return Result.Success(tweetsDto);
    }
}
