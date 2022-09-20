using EducationPortal.DataAccess.DomainModels;
using EducationPortal.DataAccess.DomainModels.Materials;
using FluentValidation;

namespace EducationPortal.BusinessLogic.Validators.FluentValidation;

public class ArticleMaterialValidator : AbstractValidator<ArticleMaterial>
{
    public ArticleMaterialValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Source).NotEmpty();
    }
}