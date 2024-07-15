using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Application.ExternalServices;
using SenaThreads.Application.IRepositories;
using SenaThreads.Application.IServices;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.UserQueries.GetUserProfile;
public class GetUserProfileQueryHandler : IQueryHandler<GetUserProfileQuery, UserProfileDto>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IAwsS3Service _awsS3Service;
    private readonly IBlockFilterService _blockFilterService;
    private readonly ICurrentUserService _currentUserService;

    public GetUserProfileQueryHandler(
        IMapper mapper,
        UserManager<User> userManager,
        IAwsS3Service awsS3Service,
        IBlockFilterService blockFilterService,
        ICurrentUserService currentUserService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _awsS3Service = awsS3Service;
        _blockFilterService = blockFilterService;
        _currentUserService = currentUserService;
    }

    public async Task<Result<UserProfileDto>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        User user = await _userManager.FindByIdAsync(request.UserId);

        if (user is null)
        {
            return Result.Failure<UserProfileDto>(UserError.UserNotFound);
        }

        var currentUserId = _currentUserService.UserId;
        var filteredContent = await _blockFilterService.FilterBlockedContent(new[] { user }, currentUserId, u => u.Id);

        UserProfileDto userProfileDto;

        if (!filteredContent.Any())
        {
            // Si el contenido está bloqueado, devolver un DTO con información básica.
            userProfileDto = new UserProfileDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                // Los demás campos se dejan en blanco o con valores predeterminados.
            };

            return Result.Success(userProfileDto);
        }

        // Mapear la entidad de usuario a un DTO de perfil
        userProfileDto = _mapper.Map<UserProfileDto>(user);

        if (!string.IsNullOrEmpty(user.ProfilePictureS3Key))
        {
            userProfileDto.ProfilePictureS3key = _awsS3Service.GeneratepresignedUrl(user.ProfilePictureS3Key);
        }

        // Retornar el DTO de perfil de usuario
        return Result.Success(userProfileDto);
    }
}

