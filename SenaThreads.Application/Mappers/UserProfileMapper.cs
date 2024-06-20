using AutoMapper;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Mappers;
public class UserProfileMapper : Profile
{
    public UserProfileMapper()
    {
        CreateMap<User, UserProfileDto>();
        CreateMap<User, UserRegistrationInfoDto>();
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(source => source.Id));
    }

}
