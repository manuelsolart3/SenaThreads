using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Tweets;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Tweets.Queries.GetUserRetweets;
public class GetUserRetweetsQueryHandler : IQueryHandler<GetUserRetweetsQuery, Pageable<BasicTweetInfoDto>>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IMapper _mapper;
    public GetUserRetweetsQueryHandler(ITweetRepository tweetRepository, IMapper mapper)
    {
        _tweetRepository = tweetRepository;
        _mapper = mapper;
    }

    public async Task<Result<Pageable<BasicTweetInfoDto>>> Handle(GetUserRetweetsQuery request, CancellationToken cancellationToken)
    {
        var paginatedRetweets = await FetchData(request.UserId, request.Page, request.PageSize);
        return Result.Success(paginatedRetweets);
    }

    private async Task<Pageable<BasicTweetInfoDto>> FetchData(string userId, int page, int pageSize)
    {
        int start = pageSize * (page - 1);

        IQueryable<Tweet> tweetsQuery = _tweetRepository.Queryable()
            .Include(t => t.User) // Incluir información del usuario creador del tweet
            .Include(t => t.Attachments) // Incluir adjuntos del tweet
            .Include(t => t.Reactions) // Incluir reacciones del tweet
            .Include(t => t.Comments) // Incluir comentarios del tweet
            .Where(t => t.Retweets.Any(r => r.RetweetedById == userId)) // Filtrar por retweets del usuario
            .OrderByDescending(t => t.CreatedOnUtc); // Ordenar por fecha de creación descendente

        int totalCount = await tweetsQuery.CountAsync();

        List<Tweet> pagedRetweets = await tweetsQuery
            .Skip(start)
            .Take(pageSize)
            .ToListAsync();

        List<BasicTweetInfoDto> retweetDtos = _mapper.Map<List<BasicTweetInfoDto>>(pagedRetweets);

        return new Pageable<BasicTweetInfoDto>
        {
            List = retweetDtos,
            Count = totalCount
        };
    }
}
