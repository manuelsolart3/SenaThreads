using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.UserQueries.GetUserRegistrationInfo;
public class GetUserRegistrationInfoQueryHandler : IQueryHandler<GetUserRegistrationInfoQuery, UserRegistrationInfoDto>
{
    private readonly UserManager<User> _userManager; 
    private readonly IMapper _mapper;

    public GetUserRegistrationInfoQueryHandler(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<Result<UserRegistrationInfoDto>> Handle(GetUserRegistrationInfoQuery request, CancellationToken cancellationToken)
    {
        User user = await _userManager.FindByIdAsync(request.UserId);
        if (user is null)
        {
            return (Result<UserRegistrationInfoDto>)Result.Failure(UserError.UserNotFound);
        }

        UserRegistrationInfoDto userDto = _mapper.Map<UserRegistrationInfoDto>(user);

        return Result.Success(userDto);
    }
}
