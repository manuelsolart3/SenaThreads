using FluentValidation;

namespace SenaThreads.Application.Notifications.Commands.SendNotification;
public class SendNotificationValidator : AbstractValidator<SendNotificationCommand>
{
    public SendNotificationValidator()
    {
        RuleFor(x => x.notifierUserId).NotEmpty().WithMessage("UserId is required.");
        RuleFor(x => x.type).NotEmpty().WithMessage("Type is required.");
        RuleFor(x => x.path).NotEmpty().WithMessage("Path is required.");
          
    }
}
