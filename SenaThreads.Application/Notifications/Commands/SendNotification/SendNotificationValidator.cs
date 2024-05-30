using FluentValidation;

namespace SenaThreads.Application.Notifications.Commands.SendNotification;
public class SendNotificationValidator : AbstractValidator<SendNotificationCommand>
{
    public SendNotificationValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
        RuleFor(x => x.Type).NotEmpty().WithMessage("Type is required.");
        RuleFor(x => x.Path).NotEmpty().WithMessage("Path is required.");
          
    }
}
