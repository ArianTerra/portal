using EducationPortalConsole.Core.Entities;
using EducationPortalConsole.DataAccess.Repositories;

namespace EducationPortalConsole.BusinessLogic.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService()
    {
        _repository = new UserRepository("Users");
    }

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public User? GetById(Guid id)
    {
        return _repository.FindFirst(x => x.Id == id);
    }

    public User? GetByName(string name)
    {
        return _repository.FindFirst(x => x.Name == name);
    }

    public IEnumerable<User> GetAll()
    {
        return _repository.GetAll();
    }

    public void Add(User user)
    {
        _repository.Add(user);
    }

    public void Update(User user)
    {
        _repository.Update(user);
    }

    public bool Delete(User user)
    {
        return _repository.Delete(user);
    }
}