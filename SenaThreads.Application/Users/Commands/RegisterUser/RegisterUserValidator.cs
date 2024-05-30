using FluentValidation;
using Microsoft.AspNet.Identity;
using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Users.Commands.RegisterUser;
public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("The Name is Required");

        RuleFor(x => x.LastName).NotEmpty().WithMessage("the Lastname is Required");
    }  
}
