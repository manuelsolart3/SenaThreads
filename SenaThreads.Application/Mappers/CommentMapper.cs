using AutoMapper;
using SenaThreads.Application.Dtos.Notifications;
using SenaThreads.Application.Dtos.Tweets;
using SenaThreads.Domain.Notifications;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Mappers;
public class CommentMapper : Profile
{
    public CommentMapper()
    {
        CreateMap<Comment, CommentDto>()

        //Mapeo del usuario creador del comment
        .ForMember(dest => dest.CommentId, opt => opt.MapFrom(source => source.Id))
        .ForMember(dest => dest.UserName, opt => opt.MapFrom(source => source.User.UserName))
        .ForMember(dest => dest.FirstName, opt => opt.MapFrom(source => source.User.FirstName))
        .ForMember(dest => dest.LastName, opt => opt.MapFrom(source => source.User.LastName))
        .ForMember(dest => dest.ProfilePictureS3key, opt => opt.MapFrom(source => source.User.ProfilePictureS3Key));
    }
}
