using AutoMapper;
using SenaThreads.Application.Dtos.Events;
using SenaThreads.Domain.Events;

namespace SenaThreads.Application.Mappers;
public class EventMapper : Profile
{

    public EventMapper () 
    {
        CreateMap<Event, EventDto>()
              // Mapeo de las propiedades del usuario creador del evento
              .ForMember(dest => dest.UserId, opt => opt.MapFrom(source => source.User.Id))
              .ForMember(dest => dest.UserName, opt => opt.MapFrom(source => source.User.UserName))
              .ForMember(dest => dest.FirstName, opt => opt.MapFrom(source => source.User.FirstName))
              .ForMember(dest => dest.LastName, opt => opt.MapFrom(source => source.User.LastName))
              .ForMember(dest => dest.ProfilePictureS3key, opt => opt.MapFrom(source => source.User.ProfilePictureS3Key))
              .ForMember(dest => dest.EventId, opt => opt.MapFrom(source => source.Id))

            //Mapeo de la prpiedad de entity para la fecha de creacion
            .ForMember(dest => dest.CreatedOnUtc, opt => opt.MapFrom(source => source.CreatedOnUtc));



    }
}
