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

namespace SenaThreads.Application.Tweets.Queries.GetTweetByIdQuery;
public class GetTweetByIdQueryHandler : IQueryHandler<GetTweetByIdQuery, BasicTweetInfoDto>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IMapper _mapper;
    private readonly IAwsS3Service _awsS3Service;
    private readonly IBlockFilterService _blockFilterService;
    private readonly ICurrentUserService _currentUserService;

    public GetTweetByIdQueryHandler(ITweetRepository tweetRepository, IMapper mapper, IAwsS3Service awsS3Service, IBlockFilterService blockFilterService, ICurrentUserService currentUserService)
    {
        _tweetRepository = tweetRepository;
        _mapper = mapper;
        _awsS3Service = awsS3Service;
        _blockFilterService = blockFilterService;
        _currentUserService = currentUserService;
    }

    public async Task<Result<BasicTweetInfoDto>> Handle(GetTweetByIdQuery request, CancellationToken cancellationToken)
    {
       var currentUserId = _currentUserService.UserId;
        
        Tweet tweet = await FetchData(request.tweetId);

        if (tweet is null)
        {
            return Result.Failure<BasicTweetInfoDto>(TweetError.NotFound);
        }



        var tweetDto = _mapper.Map<BasicTweetInfoDto>(tweet);
        var filteredTweetDto = await _blockFilterService.FilterBlockedContent(new List<BasicTweetInfoDto> { tweetDto }, currentUserId, t => t.UserId);

        if (!filteredTweetDto.Any())
        {
            return Result.Failure<BasicTweetInfoDto>(UserError.UserBlocked);
        }

        var basicTweetInfoDto = filteredTweetDto.First();
        // Gestionar las imágenes adjuntas
        foreach (var attachment in basicTweetInfoDto.Attachments)
        {
            attachment.presignedUrl = _awsS3Service.GeneratepresignedUrl(attachment.key);
        }

        if (!string.IsNullOrEmpty(tweetDto.ProfilePictureS3key))
        {
            tweetDto.ProfilePictureS3key = _awsS3Service.GeneratepresignedUrl(tweetDto.ProfilePictureS3key);
        }

        return Result.Success(basicTweetInfoDto);
    }

    private async Task<Tweet> FetchData(Guid tweetId)
    {
        return await _tweetRepository.Queryable()
            .Include(t => t.User)
            .Include(t => t.Attachments)
            .Include(t => t.Reactions)
            .Include(t => t.Retweets)
            .Include(t => t.Comments)
            .FirstOrDefaultAsync(t => t.Id == tweetId);
    }
}
