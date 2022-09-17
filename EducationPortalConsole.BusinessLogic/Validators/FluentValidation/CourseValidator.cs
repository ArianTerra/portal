using EducationPortalConsole.Core.Entities;
using FluentValidation;

namespace EducationPortalConsole.BusinessLogic.Validators.FluentValidation;

public class CourseValidator : AbstractValidator<Course>
{
    public CourseValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}