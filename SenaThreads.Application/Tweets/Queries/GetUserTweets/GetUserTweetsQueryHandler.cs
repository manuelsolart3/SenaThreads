using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Tweets;
using SenaThreads.Application.ExternalServices;
using SenaThreads.Application.IRepositories;
using SenaThreads.Application.IServices;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Tweets.Queries.GetUserTweets;
public class GetUserTweetsQueryHandler : IQueryHandler<GetUserTweetsQuery, Pageable<BasicTweetInfoDto>>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IMapper _mapper;
    private readonly IAwsS3Service _awsS3Service;
    private readonly IBlockFilterService _blockFilterService;
    private readonly ICurrentUserService _currentUserService;

    public GetUserTweetsQueryHandler(
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

    public async Task<Result<Pageable<BasicTweetInfoDto>>> Handle(GetUserTweetsQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId;
        var paginatedTweets = await FetchData(request.UserId, request.Page, request.PageSize, currentUserId);

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

    private async Task<Pageable<BasicTweetInfoDto>> FetchData(string userId, int page, int pageSize, string currentUserId)
    {
        int start = pageSize * (page - 1);

        IQueryable<Tweet> tweetsQuery = _tweetRepository.Queryable()
            .Include(t => t.User)
            .Include(t => t.Attachments)
            .Include(t => t.Reactions)
            .Include(t => t.Retweets)
            .Include(t => t.Comments)
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.CreatedOnUtc);

        List<Tweet> tweets = await tweetsQuery.ToListAsync();
        List<BasicTweetInfoDto> tweetDtos = _mapper.Map<List<BasicTweetInfoDto>>(tweets);

        // Aplicar el filtro de bloqueo mutuo a los tweets
        var filteredTweetDtos = await _blockFilterService.FilterBlockedContent(tweetDtos, currentUserId, t => t.UserId);

        // Calcular el total de tweets filtrados
        int totalFilteredCount = filteredTweetDtos.Count();

        // Aplicar paginación después del filtrado
        var paginatedTweetDtos = filteredTweetDtos
            .Skip(start)
            .Take(pageSize)
            .ToList();

        return new Pageable<BasicTweetInfoDto>
        {
            List = paginatedTweetDtos,
            Count = totalFilteredCount
        };
    }
}
