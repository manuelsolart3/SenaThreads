using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Application.ExternalServices;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.UserQueries.GetUserFollowed;
public class GetUserFollowedQueryHandler : IQueryHandler<GetUserFollowedQuery, FollowerResultDto>
{
    private readonly IFollowRepository _followRepository;
    private readonly IMapper _mapper;
    private readonly IAwsS3Service _awsS3Service;

    public GetUserFollowedQueryHandler(
        IFollowRepository followRepository,
        IMapper mapper, 
        IAwsS3Service awsS3Service)
    {
        _followRepository = followRepository;
        _mapper = mapper;
        _awsS3Service = awsS3Service;
    }
    public async Task<Result<FollowerResultDto>> Handle(GetUserFollowedQuery request, CancellationToken cancellationToken)
    {
        var (followedUsers, totalCount) = await FetchData(request.UserId, request.Limit);

        foreach (var followed in followedUsers)
        {
            if (!string.IsNullOrEmpty(followed.ProfilePictureS3key))
            {
                followed.ProfilePictureS3key = _awsS3Service.GeneratepresignedUrl(followed.ProfilePictureS3key);
            }
        }

        return Result.Success(new FollowerResultDto(followedUsers, totalCount));
    }

    private async Task<(List<FollowerDto>, int)> FetchData(string userId, int? limit)
    {
        var followedsQuery = _followRepository.Queryable()
            .Where(f => f.FollowerUserId == userId)
            .Include(u => u.FollowedByUser)
            .Select(f => f.FollowedByUser);

        int totalCount = await followedsQuery.CountAsync();

        if (limit.HasValue)
        {
            followedsQuery = followedsQuery.Take(limit.Value);
        }

        var followeds = await followedsQuery.ToListAsync();
        var followerDtos = _mapper.Map<List<FollowerDto>>(followeds);

        return (followerDtos, totalCount);
    }
}
