using AutoMapper;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Dtos.Notifications;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Notifications;

namespace SenaThreads.Application.Notifications.QueriesN.GetUserNotifications;
public class GetUserNotificationsQueryHandler : IQueryHandler<GetUserNotificationsQuery, List<NotificationDto>>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IMapper _mapper;

    public GetUserNotificationsQueryHandler(INotificationRepository notificationRepository, IMapper mapper)
    {
        _notificationRepository = notificationRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<NotificationDto>>> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken)
    {
        var usernotification = await _notificationRepository.GetUserNotificationsAsync(request.UserId);

        var userNDtos = _mapper.Map<List<NotificationDto>>(usernotification);

        return Result.Success(userNDtos);

    }
}
