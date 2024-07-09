using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Notifications;

namespace SenaThreads.Application.Notifications.Commands.MarkNotificationAsRead;
public class MarkNotificationAsReadCommandHandler : ICommandHandler<MarkNotificationAsReadCommand>
{ 
    private readonly INotificationRepository _notificationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MarkNotificationAsReadCommandHandler(INotificationRepository notificationRepository, IUnitOfWork unitOfWork)
    {
        _notificationRepository = notificationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result>Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
    {
        //Obtener la notificacion del reposiotrio
        Notification notification = await _notificationRepository.GetByIdAsync(request.NotificationId);

        if (notification is null)
        {
            return Result.Failure(NotificationError.NotificationNotFound);//No se encontro la notificaion
        }

        // Validar si el usuario tiene permiso para marcar la notificación como leída
        if (notification.NotifierUserId != request.UserId)
        {
            return Result.Failure(NotificationError.Unauthorized); // Usuario no autorizado para marcar la notificación
        }

        //Validar si la notificacion ya fue leida
        if (notification.IsRead)
        {
            return Result.Failure(NotificationError.NotificationAlreadyRead); //Ya fue leida
        }

        //Marcamos la notificacion como leida
        notification.IsRead = true; 

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
