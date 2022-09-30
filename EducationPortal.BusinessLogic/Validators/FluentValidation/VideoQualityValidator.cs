using EducationPortal.DataAccess.DomainModels.AdditionalModels;
using FluentValidation;

namespace EducationPortal.BusinessLogic.Validators.FluentValidation;

public class VideoQualityValidator : AbstractValidator<VideoQuality>
{
    public VideoQualityValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}