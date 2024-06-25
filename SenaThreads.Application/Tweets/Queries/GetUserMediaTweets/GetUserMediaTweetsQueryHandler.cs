using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Tweets;
using SenaThreads.Application.ExternalServices;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Tweets.Queries.GetUserMediaTweets;
public class GetUserMediaTweetsQueryHandler : IQueryHandler<GetUserMediaTweetsQuery, Pageable<BasicTweetInfoDto>>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IMapper _mapper;
    private readonly IAwsS3Service _awsS3Service;

    public GetUserMediaTweetsQueryHandler(
        ITweetRepository tweetRepository,
        IMapper mapper,
        IAwsS3Service awsS3Service)
    {
        _tweetRepository = tweetRepository;
        _mapper = mapper;
        _awsS3Service = awsS3Service;
    }

    public async Task<Result<Pageable<BasicTweetInfoDto>>> Handle(GetUserMediaTweetsQuery request, CancellationToken cancellationToken)
    {
        var paginatedTweets = await FetchData(request.UserId, request.Page, request.PageSize);


        foreach (var tweet in paginatedTweets.List)
        {
            foreach(var attachment in tweet.Attachments)
            {
                attachment.PresignedUrl = _awsS3Service.GeneratePresignedUrl(attachment.Key);
            }
        }

        return Result.Success(paginatedTweets);
    }


    // Método para realizar la consulta y paginación de tweets del usuario
    private async Task<Pageable<BasicTweetInfoDto>> FetchData(string userId, int page, int pageSize)
    {
        int start = pageSize * (page - 1); // Calcular el índice de inicio para la paginación

        // Construir la consulta de tweets para el usuario específico
        IQueryable<Tweet> tweetsQuery = _tweetRepository.Queryable()
          .Include(t => t.User) // Incluir información del usuario creador del tweet
          .Include(t => t.Attachments) // Incluir adjuntos del tweet
          .Include(t => t.Reactions) // Incluir reacciones del tweet
          .Include(t => t.Retweets) // Incluir retweets del tweet
          .Include(t => t.Comments) // Incluir comentarios del tweet
          .Where(t => t.UserId == userId && t.Attachments.Any())
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

