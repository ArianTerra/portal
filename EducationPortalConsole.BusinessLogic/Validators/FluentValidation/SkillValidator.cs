using EducationPortalConsole.Core.Entities;
using FluentValidation;

namespace EducationPortalConsole.BusinessLogic.Validators.FluentValidation;

public class SkillValidator : AbstractValidator<Skill>
{
    public SkillValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}