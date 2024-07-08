using Microsoft.AspNetCore.Identity;
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
    private readonly IAwsS3Service _awsS3Service;


    public SendNotificationCommandHandler(INotificationRepository notificationRepository, IUnitOfWork unitOfWork, UserManager<User> userManager, IAwsS3Service awsS3Service)
    {
        _notificationRepository = notificationRepository;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _awsS3Service = awsS3Service;
    }

    public async Task<Result>Handle(SendNotificationCommand request, CancellationToken cancellationToken)
    {
        // Buscar información adicional del usuario que envía la notificación
        User sender = await _userManager.FindByIdAsync(request.UserId);
        if (sender is null)
        {
            // Manejo de error si el usuario no existe
            return Result.Failure(UserError.UserNotFound);
        }

        // Crear nueva notificación
        Notification newNotification = new Notification(
           request.UserId,
           request.Type,
           request.Path)
        {
            
            UserName = sender.UserName,
            FirstName = sender.FirstName,
            LastName = sender.LastName,
            ProfilePictureS3Key = sender.ProfilePictureS3Key
        };

        _notificationRepository.Add(newNotification);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();

    }
}
