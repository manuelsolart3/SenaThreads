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
        var shouldFilter = await _blockFilterService.FilterBlockedContent(new[] { user }, currentUserId, u => u.Id);

        if (!shouldFilter.Any())
        {
            return Result.Failure<UserProfileDto>(UserError.UserBlocked); 
        }

        // Mapear la entidad de usuario a un DTO de perfil
        UserProfileDto userProfileDto = _mapper.Map<UserProfileDto>(user);

        if (!string.IsNullOrEmpty(user.ProfilePictureS3Key))
        {
            userProfileDto.ProfilePictureS3Key = _awsS3Service.GeneratePresignedUrl(user.ProfilePictureS3Key);
        }

        // Retornar el DTO de perfil de usuario
        return Result.Success(userProfileDto);
    }
}

