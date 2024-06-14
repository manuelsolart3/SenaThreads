using FluentValidation;

namespace SenaThreads.Application.Events.Commands.CreateEvent;
public class CreateEventValidator : AbstractValidator<CreateEventCommand>
{
    public CreateEventValidator()
    {
        RuleFor(x => x.EventDate)
            .NotNull().WithMessage("Date of event cannot be null")
            .Must(BeAValidDateOfBirth)
            .WithMessage("Date of Event must be a valid date");
    }
    private bool BeAValidDateOfBirth(DateTime eventDate)
    {
        var today = DateTime.Now.Date;
        return eventDate.Date >= today;
    }
}
