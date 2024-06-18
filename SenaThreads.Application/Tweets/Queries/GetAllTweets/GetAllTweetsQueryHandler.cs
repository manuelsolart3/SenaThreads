using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Tweets;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Tweets.Queries.GetAllTweets;
public class GetAllTweetsQueryHandler : IQueryHandler<GetAllTweetsQuery, Pageable<BasicTweetInfoDto>>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IMapper _mapper;

    public GetAllTweetsQueryHandler(ITweetRepository tweetRepository, IMapper mapper)
    {
        _tweetRepository = tweetRepository;
        _mapper = mapper;
    }

    public async Task<Result<Pageable<BasicTweetInfoDto>>> Handle(GetAllTweetsQuery request, CancellationToken cancellationToken)
    {
        var paginatedTweets = await FetchData(request.Page, request.PageSize);

        return Result.Success(paginatedTweets);
    }

    //Método para aplicar la consulta y paginacion
     private async Task<Pageable<BasicTweetInfoDto>> FetchData(int page, int pageSize)
    {
        int start = pageSize * (page - 1);

        IQueryable<Tweet> tweetsQuery = _tweetRepository.Queryable()
            .Include(t => t.User)
            .Include(t => t.Attachments)
            .Include(t => t.Reactions)
            .Include(t => t.Retweets)
            .Include(t => t.Comments);

        int totalCount = await tweetsQuery.CountAsync();

        List<Tweet> tweets = await tweetsQuery
            .Skip(start)
            .Take(pageSize)
            .ToListAsync();

        List<BasicTweetInfoDto> tweetDtos = _mapper.Map<List<BasicTweetInfoDto>>(tweets);

        return new Pageable<BasicTweetInfoDto>
        {
            List = tweetDtos,
            Count = totalCount
        };
    }
}

