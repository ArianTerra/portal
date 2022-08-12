using EducationPortalConsole.Core.Entities;

namespace EducationPortalConsole.DataAccess.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository() : base()
    {
    }
}