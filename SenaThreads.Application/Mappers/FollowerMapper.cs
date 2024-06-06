using AutoMapper;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Mappers;
public class FollowerMapper : Profile
{
    public FollowerMapper()
    {
        CreateMap<User, FollowerDto>()
         .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
         .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
         .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
         .ForMember(dest => dest.ProfilePictureS3Key, opt => opt.MapFrom(src => src.ProfilePictureS3Key));
    }

}
