using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Notifications;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Notifications;

namespace SenaThreads.Application.Notifications.QueriesN.GetUserNotifications;
public class GetUserNotificationsQueryHandler : IQueryHandler<GetUserNotificationsQuery, Pageable<NotificationDto>>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IMapper _mapper;

    public GetUserNotificationsQueryHandler(INotificationRepository notificationRepository, IMapper mapper)
    {
        _notificationRepository = notificationRepository;
        _mapper = mapper;
    }

    public async Task<Result<Pageable<NotificationDto>>> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken)
    {
        var paginatedNotifications = await FetchData(request.UserId, request.Page, request.PageSize);
        return Result.Success(paginatedNotifications);
    }

    private async Task<Pageable<NotificationDto>> FetchData(string userId, int page, int pageSize)
    {
        int start = pageSize * (page - 1);

        IQueryable<Notification> notificationsQuery = _notificationRepository.Queryable()
            .Where(n => n.UserId == userId)
            .Include(n => n.User) // Incluir la información del usuario 
            .OrderByDescending(n => n.CreatedOnUtc); 

        int totalCount = await notificationsQuery.CountAsync();

        List<Notification> pagedNotifications = await notificationsQuery
            .Skip(start)
            .Take(pageSize)
            .ToListAsync();

        List<NotificationDto> notificationDtos = _mapper.Map<List<NotificationDto>>(pagedNotifications);

        return new Pageable<NotificationDto>
        {
            List = notificationDtos,
            Count = totalCount
        };
    }
}
