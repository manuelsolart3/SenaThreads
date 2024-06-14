using FluentValidation;

namespace SenaThreads.Application.Users.Commands.UpdateProfile;
public class UpdateProfileValidator : AbstractValidator<UpadateProfileCommand>
{
    public UpdateProfileValidator()
    {
        RuleFor(x => x.DateOfBirth)
            .Must(BeAValidDateOfBirth)
            .When(x => x.DateOfBirth.HasValue)
            .WithMessage("Date of birth must be a valid date");
    }
    private bool BeAValidDateOfBirth(DateOnly? date)
    {
        if (!date.HasValue)
        {
            return true; // No aplicar validación si la fecha es nula
        }

        // La fecha debe estar entre el rango de 1900 y la fecha de hoy
        return date.Value.Year > 1900 && date < DateOnly.FromDateTime(DateTime.Today);
    }
}
