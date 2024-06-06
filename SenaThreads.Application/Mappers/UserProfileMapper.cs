using AutoMapper;
using SenaThreads.Application.Dtos.Users;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Mappers;
public class UserProfileMapper : Profile
{
    public UserProfileMapper()
    {
        CreateMap<User, UserProfileDto>();
    }

}
