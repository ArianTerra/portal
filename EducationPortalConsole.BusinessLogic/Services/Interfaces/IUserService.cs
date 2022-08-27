using EducationPortalConsole.Core.Entities;

namespace EducationPortalConsole.BusinessLogic.Services;

public interface IUserService
{
    User? GetUserById(Guid id);

    User? GetUserByName(string name);

    IEnumerable<User> GetAllUsers();

    void AddUser(User user);

    void UpdateUser(User user);

    bool DeleteUser(User user);
}