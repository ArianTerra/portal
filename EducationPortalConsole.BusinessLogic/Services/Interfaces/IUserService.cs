using EducationPortalConsole.Core.Entities;

namespace EducationPortalConsole.BusinessLogic.Services;

public interface IUserService
{
    User? GetById(Guid id);

    User? GetByName(string name);

    IEnumerable<User> GetAll();

    void Add(User user);

    void Update(User user);

    bool Delete(User user);
}