using EducationPortalConsole.BusinessLogic.Validators;
using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.DataAccess.Repositories;
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

    public User? GetUserById(Guid id)
    {
        return _repository.FindFirst(x => x.Id == id);
    }

    public User? GetUserByName(string name)
    {
        return _repository.FindFirst(x => x.Name == name);
    }

    public User? GetUserByEmail(string email)
    {
        return _repository.FindFirst(x => x.Email == email);
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _repository.FindAll(_ => true);
    }

    public bool AddUser(User user)
    {
        var validator = new UserValidator();
        var isValid = validator.Validate(user).IsValid;

        if (isValid)
        {
            _repository.Add(user);
        }

        return isValid; //TODO should return error code instead
    }

    public void UpdateUser(User user)
    {
        _repository.Update(user);
    }

    public bool DeleteUser(User user)
    {
        return _repository.Remove(user);
    }
}