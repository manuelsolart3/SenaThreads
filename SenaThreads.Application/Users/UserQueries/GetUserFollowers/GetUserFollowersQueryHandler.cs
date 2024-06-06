using AutoMapper;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.UserQueries.GetUserFollowers;
public class GetUserFollowersQueryHandler : IQueryHandler<GetUserFollowersQuery, List<FollowerDto>>
{
    private readonly IFollowRepository _followRepository;
    private readonly IMapper _mapper;

    public GetUserFollowersQueryHandler(IFollowRepository followRepository, IMapper mapper)
    {
        _followRepository = followRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<FollowerDto>>> Handle(GetUserFollowersQuery request, CancellationToken cancellationToken)
    {
        List<User> followers = await _followRepository.GetFollowersInfoAsyn(request.UserId);

        List<FollowerDto> followerDtos = _mapper.Map<List<FollowerDto>>(followers);
          
        return Result.Success(followerDtos);
    }
}
