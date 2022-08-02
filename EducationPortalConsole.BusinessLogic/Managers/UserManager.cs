using EducationPortalConsole.BusinessLogic.DomainModels;

namespace EducationPortalConsole.BusinessLogic.Managers;

internal class UserManager: BaseManager<User>
{
    internal IEnumerable<User> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    internal User GetUserByName(string username)
    {
        throw new NotImplementedException();
    }

    internal void AddUser(User user)
    {
        throw new NotImplementedException();
    }

    internal void DeleteUser(User user)
    {
        throw new NotImplementedException();
    }
    
    //TODO edit user method?
}