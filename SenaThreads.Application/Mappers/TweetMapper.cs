using AutoMapper;
using SenaThreads.Application.Dtos.Tweets;
using SenaThreads.Domain.Tweets;

namespace SenaThreads.Application.Mappers;
public class TweetMapper : Profile
{
    public TweetMapper()
    {
        CreateMap<Tweet, BasicTweetInfoDto>()
            //configuramos el mapeo de las propiedades que deben tomar una cantidad
            //y asignarla a las propiedades nuevas en el Dto
            .ForMember(dest => dest.ReactionsCount, opt => opt.MapFrom(source => source.Reactions.Count))
            .ForMember(dest => dest.RetweetsCount, opt => opt.MapFrom(source => source.Retweets.Count))
            .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(source => source.Comments.Count))
            //Mapeo de la prpiedad de entity para la fecha de creacion
            .ForMember(dest => dest.CreatedOnUtc, opt => opt.MapFrom(source => source.CreatedOnUtc))

            //Mapeo de las propiedades del usuario creador
            .ForMember(dest => dest.TweetId, opt => opt.MapFrom(source => source.Id))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(source => source.User.UserName))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(source => source.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(source => source.User.LastName));
            


        CreateMap<TweetAttachment, TweetAttachmentDto>();
    }
}
