using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.UserQueries.GetUserFollowed;
public class GetUserFollowedQueryHandler : IQueryHandler<GetUserFollowedQuery, Pageable<FollowerDto>>
{
    private readonly IFollowRepository _followRepository;
    private readonly IMapper _mapper;

    public GetUserFollowedQueryHandler(IFollowRepository followRepository, IMapper mapper)
    {
        _followRepository = followRepository;
        _mapper = mapper;
    }
    public async Task<Result<Pageable<FollowerDto>>> Handle(GetUserFollowedQuery request, CancellationToken cancellationToken)
    {
        var paginatedFollowers = await FetchData(request.UserId, request.Page, request.PageSize);

        return Result.Success(paginatedFollowers);  
    }

    //Método para aplicar la consulta y paginacion
    private async Task<Pageable<FollowerDto>> FetchData(string userId, int page, int pageSize)
    {
        int start = pageSize * (page - 1);

        IQueryable<User> followersQuery = _followRepository.Queryable()
            .Where(f => f.FollowerUserId == userId)
            .Include(u => u.FollowedByUser) //Incluir la info del usuario
            .Select(f => f.FollowedByUser); //Solo los usuarios seguidos

        //Total de todos los seguidos
        int totalCount = await followersQuery.CountAsync();

        //Paginacion
        List<User> followers = await followersQuery
            .Skip(start)
            .Take(pageSize)
            .ToListAsync();

        //Mapeo al Dto
        List<FollowerDto> followerDtos = _mapper.Map<List<FollowerDto>>(followers);

        //Objeto pageable con la lista paginada y el total de S
        return new Pageable<FollowerDto>
        {
            List = followerDtos,
            Count = totalCount
        };
    }
}
