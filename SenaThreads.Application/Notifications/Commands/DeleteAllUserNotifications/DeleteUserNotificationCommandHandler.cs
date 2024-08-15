using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.IRepositories;
using SenaThreads.Application.Notifications.Commands.DeleteUserNotification;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Notifications;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Notifications.Commands.DeleteAllUserNotifications;
public class DeleteUserNotificationCommandHandler : ICommandHandler<DeleteUserNotificationCommand>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserNotificationCommandHandler(INotificationRepository notificationRepository, IUnitOfWork unitOfWork)
    {
        _notificationRepository = notificationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteUserNotificationCommand request, CancellationToken cancellationToken)
    {
       
        var notification = await _notificationRepository.GetByIdAsync(request.NotificationId);

        
        if (notification is null)
        {
            return Result.Failure(NotificationError.NotificationNotFound);
        }

        
        if (notification.ReceiverUserId != request.UserId)
        {
            return Result.Failure(UserError.Unauthorized);
        }

        _notificationRepository.Delete(notification);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

