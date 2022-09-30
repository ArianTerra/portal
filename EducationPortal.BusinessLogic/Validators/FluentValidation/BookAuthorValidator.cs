using EducationPortal.DataAccess.DomainModels.AdditionalModels;
using FluentValidation;

namespace EducationPortal.BusinessLogic.Validators.FluentValidation;

public class BookAuthorValidator : AbstractValidator<BookAuthor>
{
    public BookAuthorValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}