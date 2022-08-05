using EducationPortalConsole.Core.Entities;

namespace EducationPortalConsole.BusinessLogic.Services;

public interface IUserService
{
    User? GetFirst(Func<User, bool> predicate);

    IEnumerable<User> FindAll(Func<User, bool> predicate);

    IEnumerable<User> GetAll();

    void Add(User entity);

    void Update(User entity);

    bool Delete(User entity);
}