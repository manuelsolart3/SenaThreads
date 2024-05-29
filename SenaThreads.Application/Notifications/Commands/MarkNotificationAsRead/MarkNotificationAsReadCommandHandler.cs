using MediatR;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.Repositories;
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
        if (notification == null)
        {
            return Result.Failure(Error.None);//No se encontro la notificaion
        }
        //Validar si la notificacion ya fue leida
        if (notification.IsRead == true)
        {
            return Result.Failure(Error.None);
        }

        //Marcamos la notificacion como leida
        notification.IsRead = true; 
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
