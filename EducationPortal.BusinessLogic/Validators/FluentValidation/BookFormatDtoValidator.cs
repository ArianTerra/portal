using EducationPortal.BusinessLogic.DTO;
using FluentValidation;

namespace EducationPortal.BusinessLogic.Validators.FluentValidation;

public class BookFormatDtoValidator : AbstractValidator<BookFormatDto>
{
    public BookFormatDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}