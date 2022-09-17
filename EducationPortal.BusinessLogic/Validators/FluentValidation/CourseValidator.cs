using EducationPortal.DataAccess.DomainModels;
using FluentValidation;

namespace EducationPortal.BusinessLogic.Validators.FluentValidation;

public class CourseValidator : AbstractValidator<Course>
{
    public CourseValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}