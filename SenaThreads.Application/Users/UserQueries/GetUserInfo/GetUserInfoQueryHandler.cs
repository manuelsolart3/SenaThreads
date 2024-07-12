using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Application.ExternalServices;
using SenaThreads.Application.IServices;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.UserQueries.GetUserInfo;

public class GetUserInfoQueryHandler : IQueryHandler<GetUserInfoQuery, UserInfoDto>
{
    private readonly UserManager<User> _userManager;
    private readonly IUserContextService _userContextService;
    private readonly IMapper _mapper;
    private readonly IAwsS3Service _awsS3Service;
    public GetUserInfoQueryHandler(UserManager<User> userManager, IUserContextService userContextService, IMapper mapper, IAwsS3Service awsS3Service)
    {
        _userManager = userManager;
        _userContextService = userContextService;
        _mapper = mapper;
        _awsS3Service = awsS3Service;
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

        // Agregar la lógica para la imagen de perfil
        if (!string.IsNullOrEmpty(user.ProfilePictureS3Key))
        {
            userInfoDto.ProfilePictureS3Key = _awsS3Service.GeneratePresignedUrl(user.ProfilePictureS3Key);
        }

        return Result.Success(userInfoDto);
    }
}
