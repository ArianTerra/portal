using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.DataAccess.Repositories;

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

    public IEnumerable<User> GetAllUsers()
    {
        return _repository.FindAll(_ => true);
    }

    public void AddUser(User user)
    {
        _repository.Add(user);
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