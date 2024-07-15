using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SenaThreads.Application.Abstractions.Messaging;
using SenaThreads.Application.ExternalServices;
using SenaThreads.Application.IRepositories;
using SenaThreads.Domain.Abstractions;
using SenaThreads.Domain.Notifications;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Notifications.Commands.SendNotification;
public class SendNotificationCommandHandler : ICommandHandler<SendNotificationCommand>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public SendNotificationCommandHandler(INotificationRepository notificationRepository, IUnitOfWork unitOfWork, UserManager<User> userManager)
    {
        _notificationRepository = notificationRepository;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<Result> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
    {
        // Buscar información del notificador
        User notifier = await _userManager.FindByIdAsync(request.notifierUserId);
        if (notifier is null)
        {
            return Result.Failure(UserError.UserNotFound);
        }

        // Verificar que el destinatario existe
        var receiverExists = await _userManager.Users.AnyAsync(u => u.Id == request.receiverUserId);
        if (!receiverExists)
        {
            return Result.Failure(UserError.UserNotFound);
        }

        // Crear nueva notificación
        Notification newNotification = new Notification(
           request.receiverUserId,
           request.notifierUserId,
           request.type,
           request.path);

        _notificationRepository.Add(newNotification);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
