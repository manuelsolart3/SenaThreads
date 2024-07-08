using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Notifications;
using SenaThreads.Application.ExternalServices;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Notifications;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Notifications.QueriesN.GetUserNotifications;
public class GetUserNotificationsQueryHandler : IQueryHandler<GetUserNotificationsQuery, Pageable<NotificationDto>>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IAwsS3Service _awsS3Service;

    public GetUserNotificationsQueryHandler(INotificationRepository notificationRepository, IMapper mapper, UserManager<User> userManager, IAwsS3Service awsS3Service)
    {
        _notificationRepository = notificationRepository;
        _mapper = mapper;
        _userManager = userManager;
        _awsS3Service = awsS3Service;
    }

    public async Task<Result<Pageable<NotificationDto>>> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken)
    {
        var paginatedNotifications = await FetchData(request.UserId, request.Page, request.PageSize);
        return Result.Success(paginatedNotifications);
    }

    private async Task<Pageable<NotificationDto>> FetchData(string userId, int page, int pageSize)
    {
        int start = pageSize * (page - 1);

        // Consultar las notificaciones del usuario incluyendo la información del usuario que envió la notificación
        IQueryable<Notification> notificationsQuery = _notificationRepository.Queryable()
            .Where(n => n.UserId == userId)
            .Include(n => n.User); // Incluir la información del usuario que envió la notificación

        int totalCount = await notificationsQuery.CountAsync();

        List<Notification> pagedNotifications = await notificationsQuery
            .OrderByDescending(n => n.CreatedOnUtc)
            .Skip(start)
            .Take(pageSize)
            .ToListAsync();

        // Mapear las notificaciones a NotificationDto incluyendo los detalles del usuario que envió la notificación
        List<NotificationDto> notificationDtos = new List<NotificationDto>();
        foreach (var notification in pagedNotifications)
        {
            // Obtener los detalles del usuario que envió la notificación
            var senderUser = await _userManager.FindByIdAsync(notification.UserId);
            if (senderUser != null)
            {
                // Mapear la notificación a NotificationDto incluyendo los detalles del usuario que envió la notificación
                var notificationDto = _mapper.Map<NotificationDto>(notification);
                notificationDto.NotifierUserName = senderUser.UserName;
                notificationDto.NotifierFirstName = senderUser.FirstName;
                notificationDto.NotifierLastName = senderUser.LastName;
                notificationDto.NotifierProfilePictureS3Key = senderUser.ProfilePictureS3Key;

                // Generar la URL firmada de la imagen del perfil si existe ProfilePictureS3Key
                if (!string.IsNullOrEmpty(senderUser.ProfilePictureS3Key))
                {
                    notificationDto.NotifierProfilePictureS3Key = _awsS3Service.GeneratePresignedUrl(senderUser.ProfilePictureS3Key);
                }

                notificationDtos.Add(notificationDto);
            }
        }

        return new Pageable<NotificationDto>
        {
            List = notificationDtos,
            Count = totalCount
        };
    }
}
