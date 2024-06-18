using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Tweets;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Tweets.Queries.GetUserTweets;
public class GetUserTweetsQueryHandler : IQueryHandler<GetUserTweetsQuery, Pageable<BasicTweetInfoDto>>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IMapper _mapper;

    public GetUserTweetsQueryHandler(ITweetRepository tweetRepository, IMapper mapper)
    {
        _tweetRepository = tweetRepository;
        _mapper = mapper;
    }

    public async Task<Result<Pageable<BasicTweetInfoDto>>> Handle(GetUserTweetsQuery request, CancellationToken cancellationToken)
    {
        var paginatedTweets = await FetchData(request.UserId, request.Page, request.PageSize);

        return Result.Success(paginatedTweets);
    }

    // Método para realizar la consulta y paginación de tweets del usuario
    private async Task<Pageable<BasicTweetInfoDto>> FetchData(string userId, int page, int pageSize)
    {
        int start = pageSize * (page - 1); // Calcular el índice de inicio para la paginación

        // Construir la consulta de tweets para el usuario específico
        IQueryable<Tweet> tweetsQuery = _tweetRepository.Queryable()
            .Include(t => t.User)       
            .Include(t => t.Attachments) 
            .Include(t => t.Reactions)   
            .Include(t => t.Retweets)    
            .Include(t => t.Comments)  
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.CreatedOnUtc);

        int totalCount = await tweetsQuery.CountAsync(); // Obtener el número total de tweets para el usuario

        List<Tweet> pagedTweets = await tweetsQuery
            .Skip(start)       
            .Take(pageSize)    
            .ToListAsync();   

        // Mapear los tweets paginados a DTOs de información básica
        List<BasicTweetInfoDto> tweetDtos = _mapper.Map<List<BasicTweetInfoDto>>(pagedTweets);

        // Retornar un objeto paginado que contiene la lista de DTOs de tweets y el total de tweets
        return new Pageable<BasicTweetInfoDto>
        {
            List = tweetDtos,
            Count = totalCount
        };
    }
}
