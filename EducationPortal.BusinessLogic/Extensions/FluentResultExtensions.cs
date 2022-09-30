using FluentResults;
using FluentValidation.Results;

namespace EducationPortal.BusinessLogic.Extensions;

public static class FluentResultExtensions
{
    public static int GetErrorCode(this Result result)
    {
        if (result.IsSuccess)
        {
            throw new ArgumentException("Result is successful");
        }

        if (!result.Errors.First().HasMetadataKey("ErrorCode"))
        {
            throw new ArgumentException("Result does not have an error code");
        }

        return (int)result.Errors.First().Metadata["ErrorCode"];
    }

    public static int GetErrorCode<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            throw new ArgumentException("Result is successful");
        }

        if (!result.Errors.First().HasMetadataKey("ErrorCode"))
        {
            throw new ArgumentException("Result does not have an error code");
        }

        return (int)result.Errors.First().Metadata["ErrorCode"];
    }

    public static ValidationResult GetValidationResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            throw new ArgumentException("Result is successful");
        }

        if (!result.Errors.First().HasMetadataKey("ValidationResult"))
        {
            throw new ArgumentException("Result does not have a validation result");
        }

        return (ValidationResult)result.Errors.First().Metadata["ValidationResult"];
    }

    public static ValidationResult GetValidationResult(this Result result)
    {
        if (result.IsSuccess)
        {
            throw new ArgumentException("Result is successful");
        }

        if (!result.Errors.First().HasMetadataKey("ValidationResult"))
        {
            throw new ArgumentException("Result does not have a validation result");
        }

        return (ValidationResult)result.Errors.First().Metadata["ValidationResult"];
    }
}