using EducationPortalConsole.BusinessLogic.Resources.ErrorMessages;
using EducationPortalConsole.BusinessLogic.Validators.FluentValidation;
using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.DataAccess.Repositories;
using FluentResults;
using FluentValidation;

namespace EducationPortalConsole.BusinessLogic.Services;

public class UserService
{
    private readonly IGenericRepository<User> _repository;

    public UserService()
    {
        _repository = new GenericRepository<User>();
    }

    public UserService(IGenericRepository<User> repository)
    {
        _repository = repository;
    }

    public Result<User> GetUserByName(string name)
    {
        var user = _repository.FindFirst(x => x.Name == name);
        return user != null
            ? Result.Ok(user)
            : Result.Fail(ErrorMessages.UsernameNotFound);
    }

    public Result<User> GetUserByEmail(string email)
    {
        var user = _repository.FindFirst(x => x.Email == email);
        return user != null
            ? Result.Ok(user)
            : Result.Fail(ErrorMessages.EmailNotFound);
    }

    public Result AddUser(User user)
    {
        var result = ValidateUser(user);

        return result.IsSuccess
            ? Result.Try(() => _repository.Add(user))
            : result;
    }

    public Result UpdateUser(User user)
    {
        var result = ValidateUser(user);

        return result.IsSuccess
            ? Result.Try(() => _repository.Update(user))
            : result;
    }

    public Result DeleteUser(User user)
    {
        if (user == null)
        {
            return Result.Fail(new Error(ErrorMessages.ModelIsNull));
        }

        return Result.Try(() => _repository.Remove(user));
    }

    private static Result ValidateUserFields(User user)
    {
        var validator = new UserValidator();

        try
        {
            validator.ValidateAndThrow(user);
        }
        catch (ValidationException e)
        {
            return Result.Fail(new Error(ErrorMessages.ValidationError).CausedBy(e));
        }

        return Result.Ok();
    }

    private Result ValidateUser(User user)
    {
        if (user == null)
        {
            return Result.Fail(new Error(ErrorMessages.ModelIsNull));
        }

        var validationResult = ValidateUserFields(user);
        var nameResult = GetUserByName(user.Name).IsFailed
            ? Result.Ok()
            : Result.Fail(ErrorMessages.UsernameTaken);
        var emailResult = GetUserByEmail(user.Email).IsFailed
            ? Result.Ok()
            : Result.Fail(ErrorMessages.EmailTaken);

        return Result.Merge(validationResult, nameResult, emailResult);
    }
}