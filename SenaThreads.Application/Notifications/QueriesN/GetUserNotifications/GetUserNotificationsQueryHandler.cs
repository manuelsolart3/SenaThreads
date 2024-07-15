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
    private readonly IAwsS3Service _awsS3Service;
    private readonly UserManager<User> _userManager;

    public GetUserNotificationsQueryHandler(INotificationRepository notificationRepository, IMapper mapper, IAwsS3Service awsS3Service, UserManager<User> userManager)
    {
        _notificationRepository = notificationRepository;
        _mapper = mapper;
        _awsS3Service = awsS3Service;
        _userManager = userManager;
    }

    public async Task<Result<Pageable<NotificationDto>>> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken)
    {
        var paginatedNotifications = await FetchData(request.userId, request.page, request.pageSize);
        return Result.Success(paginatedNotifications);
    }

    private async Task<Pageable<NotificationDto>> FetchData(string userId, int page, int pageSize)
    {
        int start = pageSize * (page - 1);

        // Consultar las notificaciones del usuario
        IQueryable<Notification> notificationsQuery = _notificationRepository.Queryable()
            .Where(n => n.ReceiverUserId == userId);

        int totalCount = await notificationsQuery.CountAsync();

        List<Notification> pagedNotifications = await notificationsQuery
            .OrderByDescending(n => n.CreatedOnUtc)
            .Skip(start)
            .Take(pageSize)
            .ToListAsync();

        // Mapear las notificaciones a NotificationDto
        List<NotificationDto> notificationDtos = new List<NotificationDto>();
        foreach (var notification in pagedNotifications)
        {
            var notificationDto = _mapper.Map<NotificationDto>(notification);
            notificationDto.NotificationId = notification.Id;

            // Buscar información del notificador
            User notifier = await _userManager.FindByIdAsync(notification.NotifierUserId);

            if (notifier != null)
            {
                notificationDto.NotifierUserId = notifier.Id;
                notificationDto.NotifierUserName = notifier.UserName;
                notificationDto.NotifierFirstName = notifier.FirstName;
                notificationDto.NotifierLastName = notifier.LastName;

                // Generar la URL firmada de la imagen del perfil si existe NotifierProfilePictureS3key
                if (!string.IsNullOrEmpty(notifier.ProfilePictureS3Key))
                {
                    notificationDto.NotifierProfilePictureS3key = _awsS3Service.GeneratepresignedUrl(notifier.ProfilePictureS3Key);
                }
            }

            notificationDtos.Add(notificationDto);
        }

        return new Pageable<NotificationDto>
        {
            List = notificationDtos,
            Count = totalCount
        };
    }
}

