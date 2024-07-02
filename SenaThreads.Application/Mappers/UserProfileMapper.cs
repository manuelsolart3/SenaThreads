using AutoMapper;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Mappers;
public class UserProfileMapper : Profile
{
    public UserProfileMapper()
    {
        CreateMap<User, UserProfileDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(source => source.Id));
        CreateMap<User, UserRegistrationInfoDto>();
        CreateMap<User, UserInfoDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(source => source.Id));
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(source => source.Id))
            .ForMember(dest => dest.ProfilePictureS3Key, opt => opt.MapFrom(src => src.ProfilePictureS3Key));
    }

}
