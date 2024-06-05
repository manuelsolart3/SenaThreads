using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Notifications;

namespace SenaThreads.Application.Notifications.Commands.SendNotification;
public class SendNotificationCommandHandler : ICommandHandler<SendNotificationCommand>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SendNotificationCommandHandler(INotificationRepository notificationRepository, IUnitOfWork unitOfWork)
    {
        _notificationRepository = notificationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result>Handle(SendNotificationCommand request, CancellationToken cancellationToken)
    {
        // Crear nueva notificación
        Notification newNotification = new(
           request.UserId,
           request.Type,
           request.Path);
        _notificationRepository.Add(newNotification);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();

    }
}
