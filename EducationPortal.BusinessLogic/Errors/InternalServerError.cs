using FluentResults;

namespace EducationPortal.BusinessLogic.Errors;

public class InternalServerError : Error
{
    public InternalServerError(string message) : base(message)
    {
        Metadata.Add("ErrorCode", ErrorCode.InternalServerError);
    }
}