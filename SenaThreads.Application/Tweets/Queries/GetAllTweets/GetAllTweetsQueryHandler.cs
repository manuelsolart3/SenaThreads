using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Tweets;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Application.ExternalServices;
using SenaThreads.Application.IRepositories;
using SenaThreads.Application.IServices;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Tweets.Queries.GetAllTweets;
public class GetAllTweetsQueryHandler : IQueryHandler<GetAllTweetsQuery, Pageable<BasicTweetInfoDto>>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IMapper _mapper;
    private readonly IAwsS3Service _awsS3Service;
    private readonly IBlockFilterService _blockFilterService;
    private readonly ICurrentUserService _currentUserService;

    public GetAllTweetsQueryHandler(
        ITweetRepository tweetRepository,
        IMapper mapper,
        IAwsS3Service awsS3Service,
        IBlockFilterService blockFilterService,
        ICurrentUserService currentUserService)
    {
        _tweetRepository = tweetRepository;
        _mapper = mapper;
        _awsS3Service = awsS3Service;
        _blockFilterService = blockFilterService;
        _currentUserService = currentUserService;
    }

    public async Task<Result<Pageable<BasicTweetInfoDto>>> Handle(GetAllTweetsQuery request, CancellationToken cancellationToken)
    {
        var currentUserId =  _currentUserService.UserId;

        var paginatedTweets = await FetchData(request.Page, request.PageSize, currentUserId);

        foreach (var tweet in paginatedTweets.List)
        {
            foreach (var attachment in tweet.Attachments)
            {
                attachment.PresignedUrl = _awsS3Service.GeneratePresignedUrl(attachment.Key);
            }
            if (!string.IsNullOrEmpty(tweet.ProfilePictureS3Key))
            {
                tweet.ProfilePictureS3Key = _awsS3Service.GeneratePresignedUrl(tweet.ProfilePictureS3Key);
            }
        }

        return Result.Success(paginatedTweets);
    }

    //Método para aplicar la consulta y paginacion
     private async Task<Pageable<BasicTweetInfoDto>> FetchData(int page, int pageSize, string currentUserId)
    {

        IQueryable<Tweet> tweetsQuery = _tweetRepository.Queryable()
            .Include(t => t.User)
            .Include(t => t.Attachments)
            .Include(t => t.Reactions)
            .Include(t => t.Retweets)
            .Include(t => t.Comments)
            .OrderByDescending(c => c.CreatedOnUtc);

        List<BasicTweetInfoDto> allTweetDtos = _mapper.Map<List<BasicTweetInfoDto>>(tweetsQuery);

        // Aplicar el filtro de bloqueo a todos los tweets
        var filteredTweetDtos = await _blockFilterService.FilterBlockedContent(allTweetDtos, currentUserId, t => t.UserId);

        //Calcular el total de tweets filtrados
        int totalFilteredCount = filteredTweetDtos.Count();

        //Aplicar paginacion despues del filtrado
        var paginatedTweetDtos = filteredTweetDtos
            .Skip((page -1) * pageSize)
            .Take(pageSize)
            .ToList();


        //Retorna objeto con la lista de Dtos y el total de Tweets
        return new Pageable<BasicTweetInfoDto>
        {
            List = paginatedTweetDtos,
            Count = totalFilteredCount
        };
    }
}

