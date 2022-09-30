using EducationPortal.DataAccess.DomainModels;
using FluentValidation;

namespace EducationPortal.BusinessLogic.Validators.FluentValidation;

public class SkillValidator : AbstractValidator<Skill>
{
    public SkillValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}