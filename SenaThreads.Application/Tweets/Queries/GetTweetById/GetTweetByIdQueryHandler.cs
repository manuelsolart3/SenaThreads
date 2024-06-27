using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Tweets;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Application.ExternalServices;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Tweets;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Tweets.Queries.GetTweetByIdQuery;
public class GetTweetByIdQueryHandler : IQueryHandler<GetTweetByIdQuery, BasicTweetInfoDto>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IMapper _mapper;
    private readonly IAwsS3Service _awsS3Service;

    public GetTweetByIdQueryHandler(ITweetRepository tweetRepository, IMapper mapper, IAwsS3Service awsS3Service)
    {
        _tweetRepository = tweetRepository;
        _mapper = mapper;
        _awsS3Service = awsS3Service;
    }

    public async Task<Result<BasicTweetInfoDto>> Handle(GetTweetByIdQuery request, CancellationToken cancellationToken)
    {
        Tweet tweet = await FetchData(request.TweetId);

        if (tweet is null)
        {
            return Result.Failure<BasicTweetInfoDto>(TweetError.NotFound);
        }

        BasicTweetInfoDto basicTweetInfoDto = _mapper.Map<BasicTweetInfoDto>(tweet);

        // Gestionar las imágenes adjuntas
        foreach (var attachment in basicTweetInfoDto.Attachments)
        {
            attachment.PresignedUrl = _awsS3Service.GeneratePresignedUrl(attachment.Key);
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
