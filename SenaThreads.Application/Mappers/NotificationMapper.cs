using AutoMapper;
using SenaThreads.Application.Dtos.Notifications;
using SenaThreads.Domain.Notifications;

namespace SenaThreads.Application.Mappers;
public class NotificationMapper : Profile
{
    public NotificationMapper()
    {
        CreateMap<Notification, NotificationDto>()
            // Mapeo de las propiedades del usuario que realizo la accion de notificacion
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName));
    }
}
