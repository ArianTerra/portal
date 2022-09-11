using EducationPortalConsole.Core.Entities.Materials;
using FluentValidation;

namespace EducationPortalConsole.BusinessLogic.Validators.FluentValidation;

public class BookMaterialValidator : AbstractValidator<BookMaterial>
{
    public BookMaterialValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Pages).GreaterThan(0);
        //TODO rule for year
        //TODO rule for format
    }
}