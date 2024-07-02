using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Application.ExternalServices;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.UserQueries.SearchUsersByUsername;
public class SearchUsersByUsernameQueryHandler : IQueryHandler<SearchUsersByUsernameQuery, Pageable<UserDto>>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IAwsS3Service _awsS3Service;

    public SearchUsersByUsernameQueryHandler(IMapper mapper, UserManager<User> userManager, IAwsS3Service awsS3Service)
    {
        _mapper = mapper;
        _userManager = userManager;
        _awsS3Service = awsS3Service;
    }

    public async Task<Result<Pageable<UserDto>>> Handle(SearchUsersByUsernameQuery request, CancellationToken cancellationToken)
    {
        var usersQueryable = _userManager.Users
            .Where(u => u.UserName.Contains(request.SearchTerm)) // Filtrar por usernames que contengan el término de búsqueda
            .OrderBy(u => u.UserName.StartsWith(request.SearchTerm) ? 0 : 1); // Ordenar por nombres que comienzan con el término de búsqueda primero
            

        int totalCount = await usersQueryable.CountAsync();

        var pagedUsers = await usersQueryable
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

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
