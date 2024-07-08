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
            
            .ForMember(dest => dest.NotifierUserId, opt => opt.MapFrom(src => src.User.Id))
            .ForMember(dest => dest.NotifierProfilePictureS3Key, opt => opt.MapFrom(src => src.User.ProfilePictureS3Key))
            .ForMember(dest => dest.NotifierUserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.NotifierFirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.NotifierLastName, opt => opt.MapFrom(src => src.User.LastName));
    }
}
