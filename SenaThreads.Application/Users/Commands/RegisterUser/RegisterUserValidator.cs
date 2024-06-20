using FluentValidation;

namespace SenaThreads.Application.Users.Commands.RegisterUser;
public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("The Name is Required");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("the Lastname is Required");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("The Email is Required")
            .EmailAddress().WithMessage("The Email is not valid");

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("The Username is Required")
            .MinimumLength(3).WithMessage("The Username must be at least 3 characters long");
    }  
}
