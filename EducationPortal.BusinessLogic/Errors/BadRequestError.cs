using FluentResults;

namespace EducationPortal.BusinessLogic.Errors;

public class BadRequestError : Error
{
    public BadRequestError(string message) : base(message)
    {
        Metadata.Add("ErrorCode", 400);
    }
}