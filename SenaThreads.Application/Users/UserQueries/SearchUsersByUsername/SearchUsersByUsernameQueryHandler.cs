using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Application.ExternalServices;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.UserQueries.SearchUsersByUsername;
public class SearchUsersByUsernameQueryHandler : IQueryHandler<SearchUsersByUsernameQuery, Pageable<UserDto>>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IAwsS3Service _awsS3Service;
    private readonly ISearchUserHistoryRepository _searchUserHistoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SearchUsersByUsernameQueryHandler(IMapper mapper, UserManager<User> userManager, IAwsS3Service awsS3Service, ISearchUserHistoryRepository searchUserHistoryRepository, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _userManager = userManager;
        _awsS3Service = awsS3Service;
        _searchUserHistoryRepository = searchUserHistoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Pageable<UserDto>>> Handle(SearchUsersByUsernameQuery request, CancellationToken cancellationToken)
    {
        var searchTerm = request.SearchTerm.ToLower();

        // Filtrar y ordenar a nivel de base de datos
        var usersQueryable = _userManager.Users
            .Where(u => u.UserName.ToLower().Contains(searchTerm))
            .OrderBy(u => u.UserName);

        // Obtener el conteo total de usuarios filtrados
        int totalCount = await usersQueryable.CountAsync(cancellationToken);

        // Aplicar paginación
        var pagedUsers = await usersQueryable
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var userDtos = _mapper.Map<List<UserDto>>(pagedUsers);

        // Generar URLs prefirmadas para las imágenes de perfil
        foreach (var userDto in userDtos)
        {
            if (!string.IsNullOrEmpty(userDto.ProfilePictureS3Key))
            {
                userDto.ProfilePictureS3Key = _awsS3Service.GeneratePresignedUrl(userDto.ProfilePictureS3Key);
            }
        }

        var pageableResult = new Pageable<UserDto>
        {
            List = userDtos,
            Count = totalCount
        };

        return Result.Success(pageableResult);
    }
}
