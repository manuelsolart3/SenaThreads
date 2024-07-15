using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Application.ExternalServices;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.UserQueries.GetUsersNotFollowed;
public class GetUsersNotFollowedQueryHandler : IQueryHandler<GetUsersNotFollowedQuery, Pageable<FollowerDto>>
{
    private readonly UserManager<User> _userManager;
    private readonly IAwsS3Service _awsS3Service;
    private readonly IMapper _mapper;
    private readonly IFollowRepository _followRepository;

    public GetUsersNotFollowedQueryHandler(IMapper mapper, IFollowRepository followRepository, IAwsS3Service awsS3Service, UserManager<User> userManager)
    {

        _mapper = mapper;
        _followRepository = followRepository;
        _awsS3Service = awsS3Service;
        _userManager = userManager;
    }

    public async Task<Result<Pageable<FollowerDto>>> Handle(GetUsersNotFollowedQuery request, CancellationToken cancellationToken)
    {
        var paginatedFolloweds = await FetchData(request.userId, request.page, request.pageSize, cancellationToken);

        // Generar URLs prefirmadas para las imágenes de perfil
        foreach (var followed in paginatedFolloweds.List)
        {
            if (!string.IsNullOrEmpty(followed.ProfilePictureS3key))
            {
                followed.ProfilePictureS3key = _awsS3Service.GeneratepresignedUrl(followed.ProfilePictureS3key);
            }
        }

        return Result.Success(paginatedFolloweds);
    }
    // Método para aplicar la consulta y paginación
    private async Task<Pageable<FollowerDto>> FetchData(string userId, int page, int pageSize, CancellationToken cancellationToken)
    {
        int start = pageSize * (page - 1);

        // Obtener los IDs de los usuarios que ya sigue
        var followedUserIds = await _followRepository.Queryable()
            .Where(f => f.FollowerUserId == userId)
            .Select(f => f.FollowedByUserId)
            .ToListAsync();

        // Obtener todos los usuarios excepto los que ya sigue
        IQueryable<User> usersNotFollowedQuery = _userManager.Users
            .Where(u => u.Id != userId && !followedUserIds.Contains(u.Id));

        // Total de todos los usuarios no seguidos
        int totalCount = await usersNotFollowedQuery.CountAsync();

        // Paginacion
        List<User> usersNotFollowed = await usersNotFollowedQuery
            .Skip(start)
            .Take(pageSize)
            .ToListAsync();

        // Mapeo al Dto
        List<FollowerDto> usersNotFollowedDtos = _mapper.Map<List<FollowerDto>>(usersNotFollowed);

        // Objeto pageable con la lista de Dtos y el total de usuarios no seguidos
        return new Pageable<FollowerDto>
        {
            List = usersNotFollowedDtos,
            Count = totalCount
        };
    }
}

