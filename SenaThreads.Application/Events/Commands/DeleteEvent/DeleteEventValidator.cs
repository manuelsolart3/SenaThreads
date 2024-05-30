using FluentValidation;

namespace SenaThreads.Application.Events.Commands.DeleteEvent;
public class DeleteEventValidator : AbstractValidator<DeleteEventCommand>
{
    public DeleteEventValidator()
    {
        RuleFor(x => x.EventId).NotEmpty().WithMessage("EventId is required.");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
    }
}
