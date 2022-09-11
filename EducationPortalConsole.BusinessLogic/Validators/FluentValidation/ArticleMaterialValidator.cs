using EducationPortalConsole.Core.Entities.Materials;
using FluentValidation;

namespace EducationPortalConsole.BusinessLogic.Validators;

public class ArticleMaterialValidator : AbstractValidator<ArticleMaterial>
{
    public ArticleMaterialValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Source).NotEmpty();
    }
}