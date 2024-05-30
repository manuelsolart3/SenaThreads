using FluentValidation;

namespace SenaThreads.Application.Notifications.Commands.MarkNotificationAsRead;

public class MarkNotificationAsReadValidator : AbstractValidator<MarkNotificationAsReadCommand>
{
    public MarkNotificationAsReadValidator()
    {
        RuleFor(x => x.NotificationId).NotEmpty().WithMessage("NotificationId is required.");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
    }
}
