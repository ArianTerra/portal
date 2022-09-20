using FluentResults;
using FluentValidation.Results;

namespace EducationPortal.BusinessLogic.Errors;

public class ValidationError : Error
{
    public ValidationError(ValidationResult validationResult) : base("Validation error")
    {
        Metadata.Add("ErrorCode", ErrorCode.ValidationError);
        Metadata.Add("ValidationResult", validationResult);
    }
}