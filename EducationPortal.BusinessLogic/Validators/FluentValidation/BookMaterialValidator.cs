using EducationPortal.DataAccess.DomainModels.Materials;
using FluentValidation;

namespace EducationPortal.BusinessLogic.Validators.FluentValidation;

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