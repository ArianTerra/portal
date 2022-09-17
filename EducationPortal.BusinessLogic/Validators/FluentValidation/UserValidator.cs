using EducationPortal.DataAccess.DomainModels;
using FluentValidation;

namespace EducationPortal.BusinessLogic.Validators.FluentValidation;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
    }
}