using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Application.Notifications.Commands.DeleteAllUserNotifications;
public class DeleteAllUserNotificationsCommandHandler : ICommandHandler<DeleteAllUserNotificationsCommand>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAllUserNotificationsCommandHandler(
        INotificationRepository notificationRepository,
        IUnitOfWork unitOfWork)
    {
        _notificationRepository = notificationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteAllUserNotificationsCommand request, CancellationToken cancellationToken)
    {
        var userNotifications = await _notificationRepository.Queryable()
                                    .Where(n => n.ReceiverUserId == request.UserId)
                                    .ToListAsync(cancellationToken);

        if (userNotifications.Any())
        {
            _notificationRepository.DeleteRange(userNotifications);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return Result.Success();
    }
}

