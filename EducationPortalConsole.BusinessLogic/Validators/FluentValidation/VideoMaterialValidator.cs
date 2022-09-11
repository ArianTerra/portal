using EducationPortalConsole.Core.Entities.Materials;
using FluentValidation;

namespace EducationPortalConsole.BusinessLogic.Validators.FluentValidation;

public class VideoMaterialValidator : AbstractValidator<VideoMaterial>
{
    public VideoMaterialValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}