using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Application.ExternalServices;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.UserQueries.GetUserFollowers;
public class GetUserFollowersQueryHandler : IQueryHandler<GetUserFollowersQuery, FollowerResultDto>
{
    private readonly IFollowRepository _followRepository;
    private readonly IMapper _mapper;
    private readonly IAwsS3Service _awsS3Service;

    public GetUserFollowersQueryHandler(
        IFollowRepository followRepository,
        IMapper mapper,
        IAwsS3Service awsS3Service)
    {
        _followRepository = followRepository;
        _mapper = mapper;
        _awsS3Service = awsS3Service;
    }

    public async Task<Result<FollowerResultDto>> Handle(GetUserFollowersQuery request, CancellationToken cancellationToken)
    {
        var (followerUsers, totalCount) = await FetchData(request.UserId, request.Limit);


        foreach (var follower in followerUsers)
        {
            if (!string.IsNullOrEmpty(follower.ProfilePictureS3Key))
            {
                follower.ProfilePictureS3Key = _awsS3Service.GeneratePresignedUrl(follower.ProfilePictureS3Key);
            }
        }

        return Result.Success(new FollowerResultDto(followerUsers, totalCount));
    }

    //Método para aplicar la consulta y paginacion
    private async Task<(List<FollowerDto>, int)> FetchData(string userId, int? limit)
    {
        IQueryable<User> followersQuery = _followRepository.Queryable()
            .Where(f => f.FollowedByUserId == userId)
            .Include(u => u.FollowerUser) //Incluir la info del usuario
            .Select(f => f.FollowerUser); //Solo los usuarios 

        int totalCount = await followersQuery.CountAsync();

        if (limit.HasValue)
        {
            followersQuery = followersQuery.Take(limit.Value);
        }


        var followers = await followersQuery.ToListAsync();

        var followerDtos = _mapper.Map<List<FollowerDto>>(followers);
        return (followerDtos, totalCount);
    }
}



