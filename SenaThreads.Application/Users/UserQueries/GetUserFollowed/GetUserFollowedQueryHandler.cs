using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Application.ExternalServices;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.UserQueries.GetUserFollowed;
public class GetUserFollowedQueryHandler : IQueryHandler<GetUserFollowedQuery, List<FollowerDto>>
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
    public async Task<Result<List<FollowerDto>>> Handle(GetUserFollowedQuery request, CancellationToken cancellationToken)
    {
        var followedUsers = await FetchData(request.UserId, request.Limit);


        foreach (var followed in followedUsers)
        {
            if (!string.IsNullOrEmpty(followed.ProfilePictureS3Key))
            {
                followed.ProfilePictureS3Key = _awsS3Service.GeneratePresignedUrl(followed.ProfilePictureS3Key);
            }
        }

        return Result.Success(followedUsers);  
    }

    //Método para aplicar la consulta y paginacion
    private async Task<List<FollowerDto>> FetchData(string userId, int? limit)
    {

        IQueryable<User> followedsQuery = _followRepository.Queryable()
            .Where(f => f.FollowerUserId == userId)
            .Include(u => u.FollowedByUser) //Incluir la info del usuario
            .Select(f => f.FollowedByUser);//Solo los usuarios seguidos
            
        if (limit.HasValue)
        {
            followedsQuery = followedsQuery.Take(limit.Value);
        }

        var followeds = await followedsQuery.ToListAsync();
       
        return _mapper.Map<List<FollowerDto>>(followeds);
    }
}
