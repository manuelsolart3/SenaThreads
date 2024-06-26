using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Application.IServices;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.UserQueries.GetUserInfo;

public class GetUserInfoQueryHandler : IQueryHandler<GetUserInfoQuery, UserInfoDto>
{
    private readonly UserManager<User> _userManager;
    private readonly IUserContextService _userContextService;
    private readonly IMapper _mapper;
    public GetUserInfoQueryHandler(UserManager<User> userManager, IUserContextService userContextService, IMapper mapper)
    {
        _userManager = userManager;
        _userContextService = userContextService;
        _mapper = mapper;
    }

    public async Task<Result<UserInfoDto>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        if (!_userContextService.IsAuthenticated)
        {
            return Result.Failure<UserInfoDto>(UserError.Unauthorized);
        }

        // Obtener el Id del usuario autenticado desde el contexto del usuario
        var userId = _userContextService.UserId;

        // Buscar el usuario en la base de datos 
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return Result.Failure<UserInfoDto>(UserError.UserNotFound);
        }

        // Mapear el usuario a UserInfoDto
        UserInfoDto userInfoDto = _mapper.Map<UserInfoDto>(user);

        return Result.Success(userInfoDto);
    }
}
