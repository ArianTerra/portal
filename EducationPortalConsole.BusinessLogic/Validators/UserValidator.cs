using EducationPortalConsole.Core.Entities;
using FluentValidation;

namespace EducationPortalConsole.BusinessLogic.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
    }
}