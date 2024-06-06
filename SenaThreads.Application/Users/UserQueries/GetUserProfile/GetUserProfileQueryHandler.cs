using AutoMapper;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.UserQueries.GetUserProfile;
public class GetUserProfileQueryHandler : IQueryHandler<GetUserProfileQuery, UserProfileDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserProfileQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<UserProfileDto>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        User user = await _userRepository.GetUserByIdAsync(request.UserId);

        // Mapear la entidad de usuario a un DTO de perfil
         UserProfileDto userProfileDto = _mapper.Map<UserProfileDto>(user);

        // Retornar el DTO de perfil de usuario
        return Result.Success(userProfileDto);

    }
}
