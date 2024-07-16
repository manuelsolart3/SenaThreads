using AutoMapper;
using SenaThreads.Application.Dtos.Tweets;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Mappers;
public class ReactionMapper : Profile
{
    public ReactionMapper()
    {
        CreateMap<Reaction, ReactionDto>()
             .ForMember(dest => dest.ReactionId, opt => opt.MapFrom(source => source.Id))
             .ForMember(dest => dest.UserName, opt => opt.MapFrom(source => source.User.UserName))
             .ForMember(dest => dest.FirstName, opt => opt.MapFrom(source => source.User.FirstName))
             .ForMember(dest => dest.LastName, opt => opt.MapFrom(source => source.User.LastName))
             .ForMember(dest => dest.ProfilePictureS3key, opt => opt.MapFrom(source => source.User.ProfilePictureS3Key))
             .ForMember(dest => dest.Type, opt => opt.MapFrom(source => source.Type));
    }
}
