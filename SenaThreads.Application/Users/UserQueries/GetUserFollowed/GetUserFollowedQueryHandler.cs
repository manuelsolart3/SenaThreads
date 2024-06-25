using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Application.ExternalServices;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.UserQueries.GetUserFollowed;
public class GetUserFollowedQueryHandler : IQueryHandler<GetUserFollowedQuery, Pageable<FollowerDto>>
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
    public async Task<Result<Pageable<FollowerDto>>> Handle(GetUserFollowedQuery request, CancellationToken cancellationToken)
    {
        var paginatedFolloweds = await FetchData(request.UserId, request.Page, request.PageSize);


        foreach (var followed in paginatedFolloweds.List)
        {
            if (!string.IsNullOrEmpty(followed.ProfilePictureS3Key))
            {
                followed.ProfilePictureS3Key = _awsS3Service.GeneratePresignedUrl(followed.ProfilePictureS3Key);
            }
        }

        return Result.Success(paginatedFolloweds);  
    }

    //Método para aplicar la consulta y paginacion
    private async Task<Pageable<FollowerDto>> FetchData(string userId, int page, int pageSize)
    {
        int start = pageSize * (page - 1);

        IQueryable<User> followedsQuery = _followRepository.Queryable()
            .Where(f => f.FollowerUserId == userId)
            .Include(u => u.FollowedByUser) //Incluir la info del usuario
            .Select(f => f.FollowedByUser); //Solo los usuarios seguidos

        //Total de todos los seguidos
        int totalCount = await followedsQuery.CountAsync();

        //Paginacion
        List<User> followeds = await followedsQuery
            .Skip(start)
            .Take(pageSize)
            .ToListAsync();

        //Mapeo al Dto
        List<FollowerDto> followerDtos = _mapper.Map<List<FollowerDto>>(followeds);

        //Objeto pageable con la lista de Dtos y el total de Seguidores
        return new Pageable<FollowerDto>
        {
            List = followerDtos,
            Count = totalCount
        };
    }
}
