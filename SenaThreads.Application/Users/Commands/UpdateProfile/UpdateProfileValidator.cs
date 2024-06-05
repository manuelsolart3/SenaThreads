using FluentValidation;

namespace SenaThreads.Application.Users.Commands.UpdateProfile;
public class UpdateProfileValidator : AbstractValidator<UpadateProfileCommand>
{
    public UpdateProfileValidator()
    {
        RuleFor(x => x.DateOfBirth).Must(BeAValidDateOfBirth).WithMessage("Date of birth must be a valid date");
    }
    private bool BeAValidDateOfBirth(DateOnly date)
    {
       //La fecha debe estar entre el rango de 1900 y la fecha de hoy
        return date.Year > 1900 && date < DateOnly.FromDateTime(DateTime.Today);
    }
}
