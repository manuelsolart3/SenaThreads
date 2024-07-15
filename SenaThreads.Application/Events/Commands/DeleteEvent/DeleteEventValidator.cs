using FluentValidation;

namespace SenaThreads.Application.Events.Commands.DeleteEvent;
public class DeleteEventValidator : AbstractValidator<DeleteEventCommand>
{
    public DeleteEventValidator()
    {
        RuleFor(x => x.eventId).NotEmpty().WithMessage("EventId is required.");
        RuleFor(x => x.userId).NotEmpty().WithMessage("UserId is required.");
    }
}
