using AutoMapper;
using SenaThreads.Application.Dtos.Tweets;
using SenaThreads.Domain.Tweets;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Mappers;
public class TweetMapper : Profile
{
    public TweetMapper()
    {
        CreateMap<Tweet, BasicTweetInfoDto>()
            // Configuración de las propiedades de conteo
            .ForMember(dest => dest.ReactionsCount, opt => opt.MapFrom(source => source.Reactions.Count))
            .ForMember(dest => dest.RetweetsCount, opt => opt.MapFrom(source => source.Retweets.Count))
            .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(source => source.Comments.Count))
            //Mapeo de la prpiedad de entity para la fecha de creacion
            .ForMember(dest => dest.CreatedOnUtc, opt => opt.MapFrom(source => source.CreatedOnUtc))

            //Mapeo de las propiedades del usuario creador
            .ForMember(dest => dest.TweetId, opt => opt.MapFrom(source => source.Id))
            .ForMember(dest => dest.ProfilePictureS3key, opt => opt.MapFrom(source => source.User.ProfilePictureS3Key))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(source => source.User.UserName))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(source => source.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(source => source.User.LastName));

        CreateMap<TweetAttachment, TweetAttachmentDto>();

        CreateMap<Tweet, RetweetDto>()

    .ForMember(dest => dest.ReactionsCount, opt => opt.MapFrom(source => source.Reactions.Count))
    .ForMember(dest => dest.RetweetsCount, opt => opt.MapFrom(source => source.Retweets.Count))
    .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(source => source.Comments.Count))
    .ForMember(dest => dest.CreatedOnUtc, opt => opt.MapFrom(source => source.CreatedOnUtc))
    .ForMember(dest => dest.TweetId, opt => opt.MapFrom(source => source.Id))
    .ForMember(dest => dest.ProfilePictureS3key, opt => opt.MapFrom(source => source.User.ProfilePictureS3Key))
    .ForMember(dest => dest.UserName, opt => opt.MapFrom(source => source.User.UserName))
    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(source => source.User.FirstName))
    .ForMember(dest => dest.LastName, opt => opt.MapFrom(source => source.User.LastName));
    


    }
}
