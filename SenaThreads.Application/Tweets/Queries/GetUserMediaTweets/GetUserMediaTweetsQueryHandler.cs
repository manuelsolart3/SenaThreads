using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Tweets;
using SenaThreads.Application.ExternalServices;
using SenaThreads.Application.IRepositories;
using SenaThreads.Application.IServices;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Tweets.Queries.GetUserMediaTweets;
public class GetUserMediaTweetsQueryHandler : IQueryHandler<GetUserMediaTweetsQuery, Pageable<BasicTweetInfoDto>>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IMapper _mapper;
    private readonly IAwsS3Service _awsS3Service;
    private readonly IBlockFilterService _blockFilterService;
    private readonly ICurrentUserService _currentUserService;

    public GetUserMediaTweetsQueryHandler(
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

    public async Task<Result<Pageable<BasicTweetInfoDto>>> Handle(GetUserMediaTweetsQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId;
        var paginatedTweets = await FetchData(request.userId, request.page, request.pageSize, currentUserId);

        foreach (var tweet in paginatedTweets.List)
        {
            foreach (var attachment in tweet.Attachments)
            {
                attachment.presignedUrl = _awsS3Service.GeneratepresignedUrl(attachment.key);
            }
            if (!string.IsNullOrEmpty(tweet.ProfilePictureS3key))
            {
                tweet.ProfilePictureS3key = _awsS3Service.GeneratepresignedUrl(tweet.ProfilePictureS3key);
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
            .Where(t => t.UserId == userId && t.Attachments.Any())
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


