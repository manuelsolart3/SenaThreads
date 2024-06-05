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
            .ForMember(dest => dest.ReactionsCount, opt => opt.MapFrom(src => src.Reactions.Count))
            .ForMember(dest => dest.RetweetsCount, opt => opt.MapFrom(src => src.Retweets.Count))
            .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count));

        CreateMap<TweetAttachment, TweetAttachmentDto>();
    }
}
